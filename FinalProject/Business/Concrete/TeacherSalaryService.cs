using AutoMapper;
using Business.Abstract;
using Business.DTOs.TeacherSalaryDto;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TeacherSalaryService : ITeacherSalaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeacherSalaryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CalculateSalaryAsync(int teacherId)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(teacherId);

            var now = DateTime.UtcNow;

            var attendances = await _unitOfWork.Attendances
                          .GetByTeacherAndMonthAsync(teacherId, now.Month, now.Year);

            if (!attendances.Any())
                throw new Exception("Bu ay üçün dərs yoxdur");

            var totalMinutes = attendances.Sum(x =>
            {
                var duration = (x.LeaveTime - x.JoinTime).TotalMinutes;
                return duration > 0 ? duration : 0;
            });

            var totalHours = (int)totalMinutes / 60m;

            var totalLate = attendances.Sum(x => x.LateMinutes);
            var totalEarly = attendances.Sum(x => x.EarlyLeaveMinutes);

            var gross = totalHours * teacher.HourlySalary;

            var penaltyPerMinute = 0.2m;

            var deduction = (totalLate + totalEarly) * penaltyPerMinute;

            var finalSalary = gross - deduction;

            if (finalSalary < 0)
                finalSalary = 0;

            var salary = new TeacherSalary
            {
                TeacherId = teacherId,
                TotalLessonHours = totalHours,
                GrossSalary = gross,
                TotalLateMinutes = totalLate,
                TotalEarlyLeaveMinutes = totalEarly,
                Deduction = deduction,
                FinalSalary = finalSalary,
                Month = new DateTime(now.Year, now.Month, 1)
            };

            await _unitOfWork.TeacherSalaries.AddAsync(salary);
            await _unitOfWork.SaveAsync();
        }

        public async Task<TeacherSalaryDto> GetAsync(int teacherId)
        {
            var data = (await _unitOfWork.TeacherSalaries.GetWhereAsync(x => x.TeacherId == teacherId)).LastOrDefault();
            var result = _mapper.Map<TeacherSalaryDto>(data);   
            return result;
        }

        public async Task<TeacherSalaryDto> GetByUserIdAsync(int userId)
        {
            var teacher = (await _unitOfWork.Teachers
                .GetWhereAsync(x => x.UserId == userId))
                .FirstOrDefault();

            if (teacher == null)
                throw new Exception("Teacher tapılmadı");

            return await GetAsync(teacher.Id);
        }
    }
}
