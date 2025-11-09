using AutoMapper;
using JBC.Application.Intefraces.CrudInterfaces;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Services
{
    public class RolePayService : CrudService<RolePayRatePerJobCategoryDto, RolePayRatePerJobCategory>, IRolePayService
    {
        public RolePayService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper, uow.RoleRatePerJobCategory)
        {
        }
    }
}
