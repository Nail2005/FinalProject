using AutoMapper;
using Business.Abstract;
using Business.DTOs.LessonDto;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;       

        public LessonService(IUnitOfWork unitOfWork, IMapper mapper, IAuditService auditService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task AddAsync(CreateLessonDto dto)
        {
            if (dto.StartTime == default)
                throw new Exception("StartTime mütləq göndərilməlidir");

            var entity = _mapper.Map<Lesson>(dto);

           

            await _unitOfWork.Lessons.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Create", nameof(Lesson));
        }

        public async Task<List<LessonDto>> GetAllAsync()
        {
            var data = await _unitOfWork.Lessons.GetAllAsync();

            return _mapper.Map<List<LessonDto>>(data);
        }

        public async Task<LessonDto> GetByIdAsync(int id)
        {
            var data = await _unitOfWork.Lessons.GetByIdAsync(id);

            await _auditService.AddLogAsync("Read", nameof(Lesson));
            return _mapper.Map<LessonDto>(data);
        }

        public async Task UpdateAsync(UpdateLessonDto dto)
        {
            var entity = await _unitOfWork.Lessons.GetByIdAsync(dto.Id);

            _mapper.Map(dto, entity);

            _unitOfWork.Lessons.Update(entity);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Update", nameof(Lesson));

        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Lessons.GetByIdAsync(id);

            _unitOfWork.Lessons.Delete(entity);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Delete", nameof(Lesson));
        }
    }
}
