using Business.Abstract;
using Business.DTOs.Report;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentMonthlyReportDto> GetStudentMonthlyReport(int studentId)
        {
            var now = DateTime.UtcNow;

            var student = await _unitOfWork.Students.GetByIdAsync(studentId);

            var attendances = await _unitOfWork.Attendances.GetWhereAsync(x =>
                x.StudentId == studentId &&
                x.JoinTime.Month == now.Month &&
                x.JoinTime.Year == now.Year);

            var guidances = await _unitOfWork.Guidances.GetWhereAsync(x =>
                x.StudentId == studentId);

            var report = new StudentMonthlyReportDto
            {
                StudentId = studentId,
                StudentName = student.FullName,

                TotalLessons = attendances.Count(),

                PresentCount = attendances.Count(x => x.Status == AttendanceStatus.Present),
                LateCount = attendances.Count(x => x.Status == AttendanceStatus.Late),
                AbsentCount = attendances.Count(x => x.Status == AttendanceStatus.Absent),

                TotalLateMinutes = attendances.Sum(x => x.LateMinutes),
                TotalEarlyLeaveMinutes = attendances.Sum(x => x.EarlyLeaveMinutes),

                AverageScore = guidances.Any() ? (decimal)guidances.Average(x => x.Score) : 0
            };

            return report;
        }
    }
}
