using Business.DTOs.ParentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IParentService
    {
        Task<List<ParentDto>> GetAllAsync();
        Task<ParentDto> GetByIdAsync(int id);
        Task UpdateAsync(UpdateParentDto dto);
    }
}
