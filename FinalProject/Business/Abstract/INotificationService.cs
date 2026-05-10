using Business.DTOs.NotificationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INotificationService
    {
        Task AddAsync(NotificationDto dto);
        Task<List<NotificationDto>> GetUserNotificationsAsync(int userId);
    }
}
