using Business.DTOs.CourseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICourseService
    {
        Task AddAsync(CreateCourseDto dto);   
        Task<List<CourseDto>> GetAllAsync();
        Task<CourseDto> GetByIdAsync(int id);
        Task UpdateAsync(UpdateCourseDto dto);
        Task DeleteAsync(int id);

    }
}
