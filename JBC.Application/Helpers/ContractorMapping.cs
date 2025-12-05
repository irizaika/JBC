using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Mappers
{
    public class ContractorMapping : IMapper<Contractor, ContractorDto>
    {
        public ContractorDto ToDto(Contractor c)
        {
            if (c == null) return null;
            return new ContractorDto
            {
                Id = c.Id,
                Name = c.Name,
                Status = c.Status,
                Phone = c.Phone,
                Email = c.Email,
                ShortName = c.ShortName,
                Address = c.Address,
                BankAccount = c.BankAccount,
                RoleId = c.RoleId,
                DayRate = c.DayRate
            };
        }

        public Contractor ToEntity(ContractorDto dto)
        {
            if (dto == null) return null;
            return new Contractor
            {
                Id = dto.Id,
                Name = dto.Name,
                Status = dto.Status,
                Phone = dto.Phone,
                Email = dto.Email,
                ShortName = dto.ShortName,
                Address = dto.Address,
                BankAccount = dto.BankAccount,
                RoleId = dto.RoleId,
                DayRate = dto.DayRate
            };
        }
    }
}
