using Business.Abstract;
using Business.DTOs.AttendanceDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("join")]
        public async Task<IActionResult> Mark([FromBody] AttendanceDto dto)
        {
            try
            {
                await _service.MarkAsync(dto);
                return Ok("Derse daxil oldu");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("leave")]
        public async Task<IActionResult> Leave(int attendanceId)
        {
            try
            {
                await _service.LeaveAsync(attendanceId);
                return Ok("Dersden cixdi");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Parent")]
        [HttpPost("get-students")]
        public async Task<IActionResult> GetStudentAttendance()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _service.GetByStudentAsync(userId);
            return Ok(result);
        }
    }
}