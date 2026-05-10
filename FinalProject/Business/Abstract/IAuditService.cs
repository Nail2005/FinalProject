using Business.DTOs.AuditLogDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuditService
    {
        Task AddLogAsync(string action, string entityName);
        Task<List<AuditLogDto>> GetAllAsync();
    }
}
