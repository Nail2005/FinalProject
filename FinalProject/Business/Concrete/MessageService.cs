using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MessageService : IMessageService
    {
        public Task SendAsync(string phoneNumber, string message)
        {
            return SendMessage(phoneNumber, message);
        }

        public Task SendOtpAsync(string phoneNumber, string message)
        {
            return SendMessage(phoneNumber, message);
        }

        private Task SendMessage(string phoneNumber, string message)
        {
            var url = $"https://wa.me/{phoneNumber}?text={Uri.EscapeDataString(message)}";

            Console.WriteLine(url);

            return Task.CompletedTask;
        }
    }
}
