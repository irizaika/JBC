using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Services
{
    public class PartnerService : CrudService<PartnerDto, Partner>, IPartnerService
    {
        public PartnerService(IUnitOfWork uow, IMapper<Partner, PartnerDto> mapper) : base(uow, mapper,uow.Partners)
        {
        }
    }
}
