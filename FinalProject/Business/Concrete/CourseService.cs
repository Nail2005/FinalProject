using AutoMapper;
using Business.Abstract;
using Business.DTOs.CourseDto;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IAuditService auditService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task AddAsync(CreateCourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);

            await _unitOfWork.Courses.AddAsync(entity);

            await _unitOfWork.SaveAsync();

            await _auditService.AddLogAsync("Create", nameof(Course));

        }

        public async Task<List<CourseDto>> GetAllAsync()
        {
            var data = await _unitOfWork.Courses.GetAllAsync();
            return _mapper.Map<List<CourseDto>>(data);
        }

        public async Task<CourseDto> GetByIdAsync(int id)
        {
            var data = await _unitOfWork.Courses.GetByIdAsync(id);

            await _auditService.AddLogAsync("Read", nameof(Course));
            return _mapper.Map<CourseDto>(data);
        }

        public async Task UpdateAsync(UpdateCourseDto dto)
        {
            var entity = await _unitOfWork.Courses.GetByIdAsync(dto.Id);

            _mapper.Map(dto, entity);

            _unitOfWork.Courses.Update(entity);
            await _unitOfWork.SaveAsync();

            await _auditService.AddLogAsync("Update", nameof(Course));
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Courses.GetByIdAsync(id);

            _unitOfWork.Courses.Delete(entity);
            await _unitOfWork.SaveAsync();

            await _auditService.AddLogAsync("Delete", nameof(Course));
        }
    }
}
