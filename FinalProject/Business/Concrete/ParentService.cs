using AutoMapper;
using Business.Abstract;
using Business.DTOs.ParentDto;
using DAL.Abstract;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public ParentService(IUnitOfWork unitOfWork, IMapper mapper, IAuditService auditService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task<ParentDto> GetByIdAsync(int id)
        {
            var data = await _unitOfWork.Parents.GetByIdAsync(id);
            await _auditService.AddLogAsync("Read", nameof(Parent));
            return _mapper.Map<ParentDto>(data);
        }

        public async Task<List<ParentDto>> GetAllAsync()
        {
            var data = await _unitOfWork.Parents.GetAllAsync();
            return _mapper.Map<List<ParentDto>>(data);
        }

        public async Task UpdateAsync(UpdateParentDto dto)
        {
            var entity = await _unitOfWork.Parents.GetByIdAsync(dto.Id);

            _mapper.Map(dto, entity);

            _unitOfWork.Parents.Update(entity);
            await _unitOfWork.SaveAsync();
            await _auditService.AddLogAsync("Update", nameof(Parent));
        }
    }
}
