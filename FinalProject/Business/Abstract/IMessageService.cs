using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMessageService
    {
        Task SendOtpAsync(string phoneNumber, string message);
        Task SendAsync(string phoneNumber, string message);
    }
}
