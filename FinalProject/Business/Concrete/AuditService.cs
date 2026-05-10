using AutoMapper;
using Business.Abstract;
using Business.DTOs.AuditLogDto;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuditService : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuditService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddLogAsync(string action, string entityName)
        {
            await _unitOfWork.AuditLogs.AddAsync(new AuditLog
            {
                Action = action,
                TableName = entityName,
                UserEmail = "system",
                CreatedDate = DateTime.Now
            });

            await _unitOfWork.SaveAsync();
        }

        public async Task<List<AuditLogDto>> GetAllAsync()
        {
            var data = await _unitOfWork.AuditLogs.GetAllAsync();

            return _mapper.Map<List<AuditLogDto>>(data);
        }
    }
}
