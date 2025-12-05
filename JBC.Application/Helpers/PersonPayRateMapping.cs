using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Mappers
{
    public class PersonPayRateMapping : IMapper<PersonPayRatePerJobType, PersonPayRatePerJobTypeDto>
    {
        public PersonPayRatePerJobTypeDto ToDto(PersonPayRatePerJobType rate)
        {
            if (rate == null) return null;
            return new PersonPayRatePerJobTypeDto
            {
                Id = rate.Id,
                JobTypeId = rate.JobTypeId,
                Pay = rate.Pay,
                ContractorId = rate.ContractorId
            };
        }

        public PersonPayRatePerJobType ToEntity(PersonPayRatePerJobTypeDto dto)
        {
            if (dto == null) return null;
            return new PersonPayRatePerJobType
            {
                Id = dto.Id,
                JobTypeId = dto.JobTypeId,
                ContractorId = dto.ContractorId,
                Pay = dto.Pay
            };
        }
    }
}
