using Business.DTOs.LessonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILessonService
    {
        Task AddAsync(CreateLessonDto dto);
        Task<List<LessonDto>> GetAllAsync();
        Task<LessonDto> GetByIdAsync(int id);
        Task UpdateAsync(UpdateLessonDto dto);
        Task DeleteAsync(int id);

    }
}
