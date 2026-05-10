using Business.DTOs.GuidanceDto;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGuidanceService
    {
        Task AddAsync(CreateGuidanceDto dto);
        Task<List<GuidanceDto>> GetByStudentAsync(int studentId);
    }
}
