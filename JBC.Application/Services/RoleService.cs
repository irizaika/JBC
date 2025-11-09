using AutoMapper;
using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Services
{
    public class RoleService : CrudService<RoleDto, Role>, IRoleService
    {
        public RoleService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper, uow.Roles)
        {
        }
    }
}
