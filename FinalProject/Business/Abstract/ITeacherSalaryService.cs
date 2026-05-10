using Business.DTOs.TeacherSalaryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITeacherSalaryService
    {
        Task CalculateSalaryAsync(int teacherId);
        Task<TeacherSalaryDto> GetAsync(int teacherId);
        Task<TeacherSalaryDto> GetByUserIdAsync(int userId);    
    }
}
