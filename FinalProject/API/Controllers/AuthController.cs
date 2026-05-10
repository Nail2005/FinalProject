using Business.Abstract;
using Business.DTOs.AuthDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateParentStudent(CreateParentStudentDto dto)
        {
            await _service.CreateParentStudentAsync(dto);
            return Ok("Parent ve Student yaradildi, OTP gonderildi");
        }

        [HttpPost("verify-otp")]    
        public async Task<IActionResult> VerifyOtp(VerifyOptDto dto)
        {
            var result = await _service.VerifyOtpAsync(dto);    

            if(!result)
            {
                return BadRequest("OTP yanlisdir");
            }
            return Ok("Hesab aktivlesdi");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _service.LoginAsync(dto.PhoneNumber);
           
            return Ok(result);  
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] string phoneNumber)
        {
            await _service.GenerateOtpAsync(phoneNumber);
            return Ok("Yeni OTP gonderildi");
        }
    }
}
