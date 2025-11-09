using JBC.Application.Interfaces;
using JBC.Domain.Dto;

namespace JBC.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _uow;

        public ReportService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<PartnerJobSummaryDto>> GetPartnerSummaryAsync(DateOnly startDate, DateOnly endDate, bool combineNoPartner)
        {
            return await _uow.Jobs.PartnerJobSummary(startDate, endDate, combineNoPartner);
        }

        public async Task<IEnumerable<ContractorReportDto>> GetContractorReportAsync(DateOnly startDate, DateOnly endDate, bool combineNoPartner)
        {
            return await _uow.Jobs.GetContractorReportAsync(startDate, endDate, combineNoPartner);
        }
    }
}
