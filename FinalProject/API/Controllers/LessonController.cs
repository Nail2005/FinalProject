using AutoMapper;
using Business.Abstract;
using Business.DTOs.LessonDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]       
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _service;
        private readonly IMapper _mapper;
        public LessonController(ILessonService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLessonDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Ders yaradildi");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lessons = await _service.GetAllAsync();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lesson = await _service.GetByIdAsync(id);
            if (lesson == null)
                return NotFound("Ders tapilmadi");
            return Ok(lesson);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLessonDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Ders yenilendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Ders silindi");
        }
    }
}
