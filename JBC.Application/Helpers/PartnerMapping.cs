using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;
using JBC.Domain.Enum;

namespace JBC.Application.Mappers
{
    public class PartnerMapping : IMapper<Partner, PartnerDto>
    {
        public PartnerDto ToDto(Partner p)
        {
            if (p == null) return null;
            return new PartnerDto
            {
                Id = p.Id,
                CompanyName = p.CompanyName,
                ShortName = p.ShortName,
                Address = p.Address,
                Email = p.Email,
                Phone1 = p.Phone1,
                Phone2 = p.Phone2,
                Status = (int)p.Status
            };
        }

        public Partner ToEntity(PartnerDto dto)
        {
            if (dto == null) return null;
            return new Partner
            {
                Id = dto.Id,
                CompanyName = dto.CompanyName,
                Address = dto.Address,
                ShortName = dto.ShortName,
                Email = dto.Email,
                Phone1 = dto.Phone1,
                Phone2 = dto.Phone2,
                Status = (ActivityStatus)dto.Status
            };
        }
    }
}
