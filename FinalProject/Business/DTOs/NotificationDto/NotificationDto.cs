using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.NotificationDto
{
    public class NotificationDto
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
