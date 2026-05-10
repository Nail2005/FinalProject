using Business.Abstract;
using Business.DTOs.TeacherSalaryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSalaryController : ControllerBase
    {
        private readonly ITeacherSalaryService _service;

        public TeacherSalaryController(ITeacherSalaryService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate(int teacherId)
        {
            await _service.CalculateSalaryAsync(teacherId);
            return Ok("Maaş hesablandı");
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("my-salary")]
        public async Task<IActionResult> GetMySalary()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _service.GetByUserIdAsync(userId);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetByTeacher(int teacherId)
        {
            var result = await _service.GetAsync(teacherId);
            return Ok(result);
        }

    }
}
