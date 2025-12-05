using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Mappers
{
    public class RoleMapping : IMapper<Role, RoleDto>
    {
        public RoleDto ToDto(Role role)
        {
            if (role == null) return null;
            return new RoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName,
            };
        }

        public Role ToEntity(RoleDto dto)
        {
            if (dto == null) return null;
            return new Role
            {
                Id = dto.Id,
                RoleName = dto.RoleName
            };
        }
    }
}
