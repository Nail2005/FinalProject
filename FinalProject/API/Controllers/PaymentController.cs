using Business.Abstract;
using Business.DTOs.PaymentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CreatePaymentDto dto)
        {
            await _service.AddPaymentAsync(dto);
            return Ok("Odenis elave edildi");
        }

        [Authorize(Roles = "Parent")]
        [HttpGet("my-payments")]
        public async Task<IActionResult> GetStudentPayments()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _service.GetStudentPaymentsAsync(userId);
            return Ok(result);
        }
    }
}
