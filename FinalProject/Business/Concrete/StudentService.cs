using AutoMapper;
using Business.Abstract;
using Business.DTOs.StudentDto;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IAuditService auditService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task AddAsync(CreateStudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Create", nameof(Student));
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Students.GetByIdAsync(id);   
            _unitOfWork.Students.Delete(entity);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Delete", nameof(Student));
        }

        public async Task<List<StudentDto>> GetAllAsync()
        {
            var values = await _unitOfWork.Students.GetAllAsync();
            var result = _mapper.Map<List<StudentDto>>(values); 
            return result;  
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var value = await _unitOfWork.Students.GetWithParentAsync(id);
            var result = _mapper.Map<StudentDto>(value);
            await _auditService.AddLogAsync("Read", nameof(Student));
            return result;
        }

        public async Task<List<StudentDto>> GetParentStudentsAsync(int parentId)
        {
            var data = await _unitOfWork.Students.GetByParentIdAsync(parentId);
            var result = _mapper.Map<List<StudentDto>>(data);
            return result;
        }

        public async Task UpdateAsync(UpdateStudentDto studentDto)
        {
            var entity = await _unitOfWork.Students.GetWithParentAsync(studentDto.Id);
            _mapper.Map(studentDto, entity);

            _unitOfWork.Students.Update(entity);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Update", nameof(Student));
        }
    }
}
