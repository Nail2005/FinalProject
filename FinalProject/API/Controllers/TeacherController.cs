using Business.Abstract;
using Business.DTOs.TeacherDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Teacher")]  
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateTeacherDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Teacher yaradildi");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);  
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTeacherDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Teacher yenilendi"); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Teacher silindi");
        }   
    }
}
