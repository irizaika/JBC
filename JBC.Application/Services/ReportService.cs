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
        public async Task<IEnumerable<JobSummaryPeriodReportDto>> GetJobSummaryReportAsync(DateOnly startDate, DateOnly endDate, string period)
        {
            return await _uow.Jobs.GetJobSummaryReportAsync(startDate, endDate, period);
        }

        public async Task<IEnumerable<CalendarChartDto>> GetJobsCalendarAsync()
        {
            return await _uow.Jobs.GetJobsCalendarAsync();
        }

        public async Task<IEnumerable<CalendarChartDto>> GetJobsCalendarAsync(int? vanId = null, int? contractorId = null, int? partnerId = null, int? jobTypeId = null)
        {
            return await _uow.Jobs.GetJobsCalendarAsync(vanId, contractorId, partnerId, jobTypeId);
        }


    }
}
