using Business.DTOs.AuthDto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task CreateParentStudentAsync(CreateParentStudentDto dto);
        Task<string> GenerateOtpAsync(string phone);
        Task<bool> VerifyOtpAsync(VerifyOptDto dto);
        Task<LoginDto> LoginAsync(string phone);
    }
}
