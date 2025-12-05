using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Mappers
{
    public class JobCategoryMapping : IMapper<JobCategory, JobCategoryDto>
    {
        public JobCategoryDto ToDto(JobCategory jobCategory)
        {
            if (jobCategory == null) return null;
            return new JobCategoryDto
            {
                Id = jobCategory.Id,
                Name = jobCategory.Name,
                Hours = jobCategory.Hours,
                IsCustom = jobCategory.IsCustom
            };
        }

        public JobCategory ToEntity(JobCategoryDto dto)
        {
            if (dto == null) return null;
            return new JobCategory
            {
                Id = dto.Id,
                Name = dto.Name,
                Hours = dto.Hours,
                IsCustom = dto.IsCustom
            };
        }
    }
}
