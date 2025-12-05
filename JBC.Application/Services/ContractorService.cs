using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;
using JBC.Application.Helpers;

namespace JBC.Application.Services
{
    public class ContractorService : CrudService<ContractorDto, Contractor>, IContractorService
    {
        public ContractorService(IUnitOfWork uow, IMapper<Contractor, ContractorDto> mapper)
            : base(uow, mapper, uow.Contractors)
        {
        }

        public override async Task<IEnumerable<ContractorDto>> GetAllAsync()
        {
            var contractors = await _uow.Contractors.GetAllAsync();

            var contractorDtos = contractors.Select(c => _mapper.ToDto(c)).ToList();

            var allRoleRates = await _uow.RoleRatePerJobCategory.GetAllAsync();
            var allContractorRates = await _uow.PersonRatesPerJobType.GetAllAsync();

            foreach (var dto in contractorDtos)
            {
                dto.RoleRates = allRoleRates
                    .Where(r => r.RoleId == dto.RoleId)
                    .ToDictionary(r => r.JobCategoryId, r => r.Pay);

                dto.ContractorRates = allContractorRates
                    .Where(c => c.ContractorId == dto.Id)
                    .ToDictionary(c => c.JobTypeId, c => c.Pay);
            }

            return contractorDtos;
        }
    }
}
