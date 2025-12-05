using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Services
{
    public class VanService : CrudService<VanDto, Van>, IVanService
    {
        public VanService(IUnitOfWork uow, IMapper<Van, VanDto> mapper) : base(uow, mapper, uow.Vans)
        {
        }
    }
}
