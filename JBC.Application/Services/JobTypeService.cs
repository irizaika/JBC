using AutoMapper;
using JBC.Application.Intefraces.CrudInterfaces;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Services
{
    public class JobTypeService : CrudService<JobTypeDto, JobType>, IJobTypeService
    {
        public JobTypeService(IUnitOfWork uow, IMapper mapper)
            : base(uow, mapper, uow.JobTypes)
        {
        }

        public override async Task<JobTypeDto> CreateAsync(JobTypeDto dto)
        {
            if (dto.PartnerId == 0)
                dto.PartnerId = null;

            return await base.CreateAsync(dto);
        }

        public override async Task UpdateAsync(int id, JobTypeDto dto)
        {
            if (dto.PartnerId == 0)
                dto.PartnerId = null;

            await base.UpdateAsync(id, dto);
        }
    }
}
