using JBC.Data.Interfaces;
using JBC.Models;
using JBC.Models.Dto;

namespace JBC.Data.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<List<Job>> GetJobsInRangeAsync(DateOnly start, DateOnly end);
        Task<List<Job>> GetDayJobsAsync(DateOnly day);
        Task<Job?> GetJobsWithRelationsAsync(int id);
        Task<List<PartnerJobSummaryDto>> PartnerJobSummary(DateOnly startDate, DateOnly endDate);
    }
}
