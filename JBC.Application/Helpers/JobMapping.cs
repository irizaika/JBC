using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Mappers
{
    public class JobMapping : IMapper<Job, JobDto>
    {
        public JobDto ToDto(Job job)
        {
            if (job == null) return null;
            return new JobDto
            {
                Id = job.Id,
                Date = job.Date,
                CustomerName = job.CustomerName,
                PayReceived = job.PayReceived,
                PartnerId = job.PartnerId,
                PartnerName = job.Partner?.CompanyName,
                JobTypeId = job.JobTypeId,
                JobTypeName = job.JobType?.Name,
                Count = job.Count,
                Details = job.Details,
                Contractors = job.JobContractors?
                   .Select(jc => new JobContractorDto
                   {
                       ContractorId = jc.ContractorId,
                       Pay = jc.Pay
                   }).ToList() ?? new List<JobContractorDto>(),
                Vans = job.JobVans?
                   .Select(jv => jv.VanId)
                   .ToList() ?? new List<int>()
            };
        }

        public Job ToEntity(JobDto dto)
        {
            if (dto == null) return null;
            return new Job
            {
                Id = dto.Id,
                Date = dto.Date,
                CustomerName = dto.CustomerName,
                PayReceived = dto.PayReceived,
                PartnerId = dto.PartnerId,
                JobTypeId = dto.JobTypeId,
                Count = dto.Count,
                Details = dto.Details,
                JobContractors = dto.Contractors?.Select(c => new JobContractor
                {
                    ContractorId = c.ContractorId,
                    Pay = c.Pay,
                    JobId = dto.Id
                }).ToList() ?? new List<JobContractor>(),
                JobVans = dto.Vans?.Select(v => new JobVan
                {
                    VanId = v,
                    JobId = dto.Id
                }).ToList() ?? new List<JobVan>()
            };
        }
    }
}
