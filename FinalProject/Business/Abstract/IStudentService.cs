using Business.DTOs.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task AddAsync(CreateStudentDto studentDto);
        Task UpdateAsync(UpdateStudentDto studentDto);
        Task DeleteAsync(int id);
        Task<List<StudentDto>> GetParentStudentsAsync(int parentId);
    }
}
