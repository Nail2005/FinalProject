using Business.DTOs.PaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        Task AddPaymentAsync(CreatePaymentDto dto);
        Task<bool> HasPaidAsync(int studentId); 
        Task<List<PaymentDto>> GetStudentPaymentsAsync(int userId);         
        
    }
}
