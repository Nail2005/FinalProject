using AutoMapper;
using Business.Abstract;
using Business.DTOs.AttendanceDto;
using Business.DTOs.NotificationDto;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly IMessageService _message;
        private readonly IAuditService _auditService;  
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public AttendanceService(
        IUnitOfWork unitOfWork,
        IPaymentService paymentService,
        IMessageService message,
        IAuditService auditService,IMapper mapper, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            _message = message;
            _auditService = auditService;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task MarkAsync(AttendanceDto dto)
        {
            var existing = (await _unitOfWork.Attendances
                .GetWhereAsync(x => x.StudentId == dto.StudentId && x.LessonId == dto.LessonId))
                .FirstOrDefault();

            if (existing != null)
                throw new Exception("Bu dərs üçün artıq iştirak qeyd olunub");

            var lesson = await _unitOfWork.Lessons.GetByIdAsync(dto.LessonId);
            if (lesson == null)
                throw new Exception("Ders tapilmadi");

            var student = await _unitOfWork.Students.GetWithParentAsync(dto.StudentId);

            if (student?.Parent == null)
                throw new Exception("Valideyn tapilmadi");

            var paid = await _paymentService.HasPaidAsync(dto.StudentId);

            if (!paid)
            {
                var absent = new Attendance
                {
                    StudentId = dto.StudentId,
                    LessonId = dto.LessonId,
                    Status = AttendanceStatus.Absent,
                    JoinTime = DateTime.UtcNow
                };

                await _unitOfWork.Attendances.AddAsync(absent);
                await _unitOfWork.SaveAsync();

                await _message.SendAsync(student.Parent.PhoneNumber,
                    "Ödəniş edilmədiyi üçün dərsə buraxılmadı ");

                await _auditService.AddLogAsync("Create", nameof(Attendance));

                return;
            }

            var now = DateTime.UtcNow;
           var startTime =  DateTime.SpecifyKind(lesson.StartTime, DateTimeKind.Utc);
            var delay = (now - startTime).TotalMinutes;

            AttendanceStatus status;

            if (delay <= 5)
                status = AttendanceStatus.Present;       
            else if (delay <= 30)
                status = AttendanceStatus.Late;          
            else
                status = AttendanceStatus.Absent;       

            var attendance = new Attendance
            {
                StudentId = dto.StudentId,
                LessonId = dto.LessonId,
                Status = status,
                JoinTime = now,
                LateMinutes = delay > 0 ? (int)delay : 0
            };

            await _unitOfWork.Attendances.AddAsync(attendance);
            await _unitOfWork.SaveAsync();

            await _message.SendAsync(student.Parent.PhoneNumber,
                $"Dərsə qoşuldu. Status: {status}");

            await _notificationService.AddAsync(new NotificationDto
            {
                UserId = student.Parent.UserId,
                Type = "Attendance",
                Message = $"Şagird dərsə qoşuldu. Status: {status}"
            });

            await _auditService.AddLogAsync("Create", nameof(Attendance));
        }

        public async Task LeaveAsync(int attendanceId)
        {
            var attendance = await _unitOfWork.Attendances.GetByIdAsync(attendanceId);

            if (attendance == null)
                throw new Exception("Attendance tapılmadı");

            if (attendance.LeaveTime != default)
                throw new Exception("Artıq çıxış edilib");

            attendance.LeaveTime = attendance.JoinTime.AddMinutes(60);
            var duration = (attendance.LeaveTime - attendance.JoinTime).TotalMinutes;

            if (duration < 0)
                duration = 0;
            
            attendance.DurationMinutes = (int)duration;
            attendance.EarlyLeaveMinutes = duration < 60 ? (int)(60 - duration) : 0;

            _unitOfWork.Attendances.Update(attendance);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<AttendanceDto>> GetByStudentAsync(int userId)
        {
        
            var parent = (await _unitOfWork.Parents
                .GetWhereAsync(x => x.UserId == userId))
                .FirstOrDefault();

            if (parent == null)
                throw new Exception("Parent tapilmadi");

            var students = await _unitOfWork.Students
                .GetWhereAsync(x => x.ParentId == parent.Id);

            var studentIds = students.Select(x => x.Id).ToList();

            var attendances = await _unitOfWork.Attendances
                .GetWhereAsync(x => studentIds.Contains(x.StudentId));

            return _mapper.Map<List<AttendanceDto>>(attendances);
        }
    }
}

