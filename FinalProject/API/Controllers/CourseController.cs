using AutoMapper;
using Business.Abstract;
using Business.DTOs.CourseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin, Teacher")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        private readonly IMapper _mapper;

        public CourseController(ICourseService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Course yaradildi");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _service.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _service.GetByIdAsync(id);
            if (course == null)
                return NotFound("Course tapilmadi");
            return Ok(course);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCourseDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Course yenilendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Course silindi");
        }       
    }
}
