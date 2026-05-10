using Business.Abstract;
using Business.DTOs.StudentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Parent,Teacher")]    
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
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
            if(data == null)
            {
                return NotFound("Student tapilmadi");   
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Student yaradildi");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Student yenilendi");
        }

        [HttpDelete("{id}")]    
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Student silindi");   
        }

        [HttpGet("parent/{parentId}")]
        public async Task<IActionResult> GetParentStudents(int parentId)
        {
            var data = await _service.GetParentStudentsAsync(parentId);
            return Ok(data);
        }
    }
}
