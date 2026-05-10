using Business.DTOs.TeacherDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITeacherService
    {
        Task AddAsync(CreateTeacherDto dto);
        Task<List<TeacherDto>> GetAllAsync();
        Task<TeacherDto> GetByIdAsync(int id);
        Task UpdateAsync(UpdateTeacherDto dto);
        Task DeleteAsync(int id);
    }
}
