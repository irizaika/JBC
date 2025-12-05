using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;
using JBC.Domain.Enum;

namespace JBC.Application.Mappers
{
    public class VanMapping : IMapper<Van, VanDto>
    {
        public VanDto ToDto(Van van)
        {
            if (van == null) return null;
            return new VanDto
            {
                Id = van.Id,
                VanName = van.VanName,
                Plate = van.Plate,
                Details = van.Details,
                Status = (int)van.Status //todo
            };
        }

        public Van ToEntity(VanDto dto)
        {
            if (dto == null) return null;
            return new Van
            {
                Id = dto.Id,
                VanName = dto.VanName,
                Plate = dto.Plate,
                Details = dto.Details,
                Status = (ActivityStatus)dto.Status //todo
            };
        }
    }
}
