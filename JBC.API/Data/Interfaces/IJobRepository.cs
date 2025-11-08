using JBC.Data.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;

namespace JBC.Data.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<List<Job>> GetJobsInRangeAsync(DateOnly start, DateOnly end);
        Task<List<Job>> GetDayJobsAsync(DateOnly day);
        Task<Job?> GetJobsWithRelationsAsync(int id);
        Task<List<PartnerJobSummaryDto>> PartnerJobSummary(DateOnly startDate, DateOnly endDate, bool combineNoPartner);
        Task<List<ContractorReportDto>> GetContractorReportAsync(DateOnly startDate, DateOnly endDate, bool combineNoPartner);
        Task<List<VanReportDto>> GetVanReportAsync(DateOnly startDate, DateOnly endDate);
        Task<List<JobSummaryReportDto>> GetJobSummaryReportAsync(DateOnly startDate, DateOnly endDate);
    }
}
