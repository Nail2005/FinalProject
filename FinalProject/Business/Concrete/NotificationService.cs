using AutoMapper;
using Business.Abstract;
using Business.DTOs.NotificationDto;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(NotificationDto dto)
        {
            var data = _mapper.Map<Notification>(dto);
            data.IsRead = false;
            await _unitOfWork.Notifications.AddAsync(data);

            await _unitOfWork.SaveAsync();
        }

        public async Task<List<NotificationDto>> GetUserNotificationsAsync(int userId)
        {
            var data = await _unitOfWork.Notifications.GetWhereAsync(x=> x.UserId == userId);
            var result = _mapper.Map<List<NotificationDto>>(data);
            return result;
        }
    }
}
