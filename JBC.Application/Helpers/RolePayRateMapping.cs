using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Mappers
{
    public class RolePayRateMapping : IMapper<RolePayRatePerJobCategory, RolePayRatePerJobCategoryDto>
    {
        public RolePayRatePerJobCategoryDto ToDto(RolePayRatePerJobCategory rate)
        {
            if (rate == null) return null;
            return new RolePayRatePerJobCategoryDto
            {
                Id = rate.Id,
                JobCategoryId = rate.JobCategoryId,
                Pay = rate.Pay,
                RoleId = rate.RoleId
            };
        }

        public RolePayRatePerJobCategory ToEntity(RolePayRatePerJobCategoryDto dto)
        {
            if (dto == null) return null;
            return new RolePayRatePerJobCategory
            {
                Id = dto.Id,
                JobCategoryId = dto.JobCategoryId,
                Pay = dto.Pay,
                RoleId = dto.RoleId
            };
        }
    }
}
