using JBC.Domain.Dto;

namespace JBC.Application.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<PartnerJobSummaryDto>> GetPartnerSummaryAsync(DateOnly startDate, DateOnly endDate, bool combineNoPartner);
        Task<IEnumerable<ContractorReportDto>> GetContractorReportAsync(DateOnly startDate, DateOnly endDate, bool combineNoPartner);
        Task<IEnumerable<JobSummaryPeriodReportDto>> GetJobSummaryReportAsync(DateOnly startDate, DateOnly endDate, string period);
        Task<IEnumerable<CalendarChartDto>> GetJobsCalendarAsync();
        Task<IEnumerable<CalendarChartDto>> GetJobsCalendarAsync(int? vanId = null, int? contractorId = null, int? partnerId = null, int? jobTypeId = null);
    }
}
