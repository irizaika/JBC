using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Services
{
    public class RolePayService : CrudService<RolePayRatePerJobCategoryDto, RolePayRatePerJobCategory>, IRolePayService
    {
        public RolePayService(IUnitOfWork uow, IMapper<RolePayRatePerJobCategory, RolePayRatePerJobCategoryDto> mapper)
            : base(uow, mapper, uow.RoleRatePerJobCategory)
        {
        }
    }
}
