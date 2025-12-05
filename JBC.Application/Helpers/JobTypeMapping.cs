using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Mappers
{
    public class JobTypeMapping : IMapper<JobType, JobTypeDto>
    {
        public JobTypeDto ToDto(JobType jobType)
        {
            if (jobType == null) return null;
            return new JobTypeDto
            {
                Id = jobType.Id,
                Description = jobType.Description,
                JobCategoryId = jobType.JobCategoryId,
                Name = jobType.Name,
                NumberOfPeople = jobType.NumberOfPeople,
                NumberOfVans = jobType.NumberOfVans,
                PartnerId = jobType.PartnerId,
                PayRate = jobType.PayRate
            };
        }

        public JobType ToEntity(JobTypeDto dto)
        {
            if (dto == null) return null;
            return new JobType
            {
                Id = dto.Id,
                Description = dto.Description,
                JobCategoryId = dto.JobCategoryId,
                Name = dto.Name,
                NumberOfPeople = dto.NumberOfPeople,
                NumberOfVans = dto.NumberOfVans,
                PartnerId = dto.PartnerId,
                PayRate = dto.PayRate
            };
        }
    }
}
