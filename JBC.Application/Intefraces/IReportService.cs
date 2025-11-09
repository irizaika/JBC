using JBC.Domain.Dto;

namespace JBC.Application.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<PartnerJobSummaryDto>> GetPartnerSummaryAsync(DateOnly startDate, DateOnly endDate, bool combineNoPartner);
        Task<IEnumerable<ContractorReportDto>> GetContractorReportAsync(DateOnly startDate, DateOnly endDate, bool combineNoPartner);
    }
}
