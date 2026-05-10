using AutoMapper;
using Business.Abstract;
using Business.DTOs.GuidanceDto;
using Business.DTOs.NotificationDto;
using DAL.Abstract;
using Entity.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GuidanceService : IGuidanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageService _message;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly INotificationService _notificationService;

        public GuidanceService(IUnitOfWork unitOfWork, IMessageService message, IMapper mapper, IAuditService auditService, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _message = message;
            _mapper = mapper;
            _auditService = auditService;
            _notificationService = notificationService;
        }

        public async Task AddAsync(CreateGuidanceDto dto)
        {
            var guidance = new Guidance
            {
                StudentId = dto.StudentId,
                LessonId = dto.LessonId,
                Score = dto.Score,
                Notes = dto.Note
            };

            await _unitOfWork.Guidances.AddAsync(guidance);
            await _unitOfWork.SaveAsync();

            var student = await _unitOfWork.Students.GetWithParentAsync(dto.StudentId);

            if (student?.Parent == null)
                throw new Exception("Valideyn tapilmadi");

            await _message.SendAsync(student.Parent.User.PhoneNumber, $"Yeni feedback: {dto.Note}");

            await _notificationService.AddAsync(new NotificationDto
            {
                UserId = student.Parent.UserId,
                Type = "Guidance",
                Message = $"Yeni feedback: {dto.Note}"
            });

            await _auditService.AddLogAsync("Create", nameof(Guidance));    
        }


        public async Task<List<GuidanceDto>> GetByStudentAsync(int studentId)
        {
            var data = await _unitOfWork.Guidances.GetWhereAsync(x=>x.StudentId==studentId);
            if(data == null)
            {
                throw new Exception("Guidance tapilmadi");

            }
            var result = _mapper.Map<List<GuidanceDto>>(data);

            await _auditService.AddLogAsync("Read", nameof(Guidance));
            return result;


        }
    }
}
