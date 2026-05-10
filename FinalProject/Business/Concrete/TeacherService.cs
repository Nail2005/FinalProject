using AutoMapper;
using Business.Abstract;
using Business.DTOs.TeacherDto;
using DAL.Abstract;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace Business.Concrete
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuditService _auditService;   

        public TeacherService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IAuditService auditService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _auditService = auditService;
        }

        public async Task AddAsync(CreateTeacherDto dto)
        {
            var user = new AppUser
            {
                UserName = dto.PhoneNumber,
                PhoneNumber = dto.PhoneNumber,
                FullName = dto.FullName,
                IsActive = true
            };

            var createResult = await _userManager.CreateAsync(user);

            if (!createResult.Succeeded)
            {
                throw new Exception(string.Join(" | ", createResult.Errors.Select(x => x.Description)));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Teacher");

            if (!roleResult.Succeeded)
            {
                throw new Exception(string.Join(" | ", roleResult.Errors.Select(x => x.Description)));
            }

            var teacher = new Teacher
            {
                UserId = user.Id,
                HourlySalary = dto.HourlySalary
            };

            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Create", nameof(Teacher));
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

            if (teacher == null)
                return;

            var user = await _userManager.FindByIdAsync(teacher.UserId.ToString());

            _unitOfWork.Teachers.Delete(teacher);
            await _unitOfWork.SaveAsync();

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            await _auditService.AddLogAsync("Delete", nameof(Teacher));
        }

        public async Task<List<TeacherDto>> GetAllAsync()
        {
            var data = await _unitOfWork.Teachers.GetAllWithUserAsync();
            var result = _mapper.Map<List<TeacherDto>>(data);
            return result;
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            var data = await _unitOfWork.Teachers.GetByIdAsync(id);
            var result = _mapper.Map<TeacherDto>(data);
            await _auditService.AddLogAsync("Read", nameof(Teacher));
            return result;
        }

        public async Task UpdateAsync(UpdateTeacherDto dto)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(dto.Id);

            if (teacher == null)
                return;

            teacher.HourlySalary = dto.HourlySalary;

            var user = await _userManager.FindByIdAsync(teacher.UserId.ToString());

            if (user != null)
            {
                user.FullName = dto.FullName;
                user.PhoneNumber = dto.PhoneNumber;
                user.UserName = dto.PhoneNumber;

                await _userManager.UpdateAsync(user);
            }

            _unitOfWork.Teachers.Update(teacher);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Update", nameof(Teacher));
        }
    }
}