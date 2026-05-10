using AutoMapper;
using Business.Abstract;
using Business.DTOs.NotificationDto;
using Business.DTOs.PaymentDto;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly IMessageService _message;
        private readonly INotificationService _notification;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IAuditService auditService, IMessageService message, INotificationService notification)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditService = auditService;
            _message = message;
            _notification = notification;
        }

        public async Task AddPaymentAsync(CreatePaymentDto dto)
        {

            var existing = (await _unitOfWork.Payments.GetWhereAsync(x => x.StudentId == dto.StudentId
                                                                    && x.DueDate.Month == dto.DueDate.Month
                                                                    && x.DueDate.Year == dto.DueDate.Year))
                                                                    .FirstOrDefault();

            if (existing != null)
            {
                throw new Exception("Bu ay üçün artıq ödəniş mövcuddur.");
            }

            var payment = new Payment
            {
                StudentId = dto.StudentId,
                Amount = dto.Amount,
                DueDate = new DateTime(dto.DueDate.Year, dto.DueDate.Month, 1),
                IsPaid = true,
                PaidDate = DateTime.UtcNow
            };


            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Create", nameof(Payment));

            var student = await _unitOfWork.Students.GetWithParentAsync(dto.StudentId);
            await _message.SendAsync(
                student.Parent.PhoneNumber,
                "Ödəniş qəbul edildi. Təşəkkür edirik."
            );

            await _notification.AddAsync(new NotificationDto
            {
                UserId = student.Parent.UserId,
                Type = "Ödəniş",
                Message = "Ödəniş qəbul edildi",
            });
        }

        public async Task<List<PaymentDto>> GetStudentPaymentsAsync(int userId)
        {

            var parent = await _unitOfWork.Parents
                           .GetWhereAsync(x => x.UserId == userId);

            var parentData = parent.FirstOrDefault();

            if (parentData == null)
                throw new Exception("Parent tapilmadi");

            var students = await _unitOfWork.Students
                .GetWhereAsync(x => x.ParentId == parentData.Id);

            var studentIds = students.Select(x => x.Id).ToList();

            var payments = await _unitOfWork.Payments
                .GetWhereAsync(x => studentIds.Contains(x.StudentId));

            return _mapper.Map<List<PaymentDto>>(payments.OrderByDescending(x=>x.DueDate));

        }


        public async Task<bool> HasPaidAsync(int studentId)
        {
            var data = (await _unitOfWork.Payments.GetWhereAsync(
                                    x => x.StudentId == studentId
                                    && x.DueDate.Month == DateTime.UtcNow.Month
                                    && x.DueDate.Year == DateTime.UtcNow.Year)).FirstOrDefault();

            return data != null && data.IsPaid;
        }
    }
}
