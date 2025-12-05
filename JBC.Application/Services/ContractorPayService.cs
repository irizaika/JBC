using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Services
{
    public class ContractorPayService : CrudService<PersonPayRatePerJobTypeDto, PersonPayRatePerJobType>, IContractorPayService
    {

        public ContractorPayService(IUnitOfWork uow, IMapper<PersonPayRatePerJobType, PersonPayRatePerJobTypeDto> mapper)
            : base(uow, mapper, uow.PersonRatesPerJobType)
        {
        }
    }
}
