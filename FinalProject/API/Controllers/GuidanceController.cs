using Business.Abstract;
using Business.DTOs.GuidanceDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]    
    public class GuidanceController : ControllerBase
    {
        private readonly IGuidanceService _service;

        public GuidanceController(IGuidanceService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> Add(CreateGuidanceDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Rehberlik elave edildi.");
        }

        [Authorize(Roles = "Parent")]
        [HttpGet("{studentId}")]    
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var result = await _service.GetByStudentAsync(studentId);
            return Ok(result);
        }
    }

}
