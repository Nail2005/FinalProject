using Business.Abstract;
using Business.DTOs.ParentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Parent")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _service;

        public ParentController(IParentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound("Parent tapilmadi");
            }
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateParentDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Parent yenilendi");
        }

    }
}
