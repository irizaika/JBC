using JBC.Data.Interfaces;
using JBC.Models;
using JBC.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JBC.Data
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(AppDbContext context) : base(context) { }

        public async Task<List<Job>> GetJobsInRangeAsync(DateOnly start, DateOnly end)
        {
            try
            {
                var res = await _dbSet
                    .Include(j => j.Partner)
                    .Include(j => j.JobType)
                    .Include(j => j.JobContractors)
                        .ThenInclude(jc => jc.Contractor) //  Load Contractor
                    .Include(j => j.JobVans)
                        .ThenInclude(jv => jv.Van)       //  Load Van
                    .Where(j => j.Date >= start && j.Date <= end)
                    .ToListAsync();

                Console.WriteLine(res.Count);

                return res;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return null;
        }

        public async Task<List<Job>> GetDayJobsAsync(DateOnly day)
        {
            return await _dbSet
                .Where(j => j.Date == day)
                .ToListAsync();
        }

        public async Task<Job?> GetJobsWithRelationsAsync(int id)
        {
            return await _dbSet
                .Include(j => j.JobContractors)
                .ThenInclude(jc => jc.Contractor)
                .Include(j => j.JobVans)
                .ThenInclude(jv => jv.Van)
                .Include(j => j.Partner)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<List<PartnerJobSummaryDto>> PartnerJobSummary(DateOnly startDate, DateOnly endDate)
        {
            var query = await _context.Jobs
                .Where(j => j.Date >= startDate && j.Date <= endDate)
                .GroupBy(j => new
                {
                    j.PartnerId,
                    PartnerName = j.Partner != null ? j.Partner.CompanyName : null,
                    j.CustomerName,
                    j.Count
                })
                .Select(g => new PartnerJobSummaryDto
                {
                    PartnerId = g.Key.PartnerId ?? 0,
                    PartnerName =
                        (!string.IsNullOrEmpty(g.Key.PartnerName)
                            ? g.Key.PartnerName
                            : !string.IsNullOrEmpty(g.Key.CustomerName)
                                ? g.Key.CustomerName
                                : "Unassigned"),   // handles nulls in both
                    TotalJobs = (double)g.Sum(x=>x.Count),
                    TotalPayReceived = g.Sum(x => x.PayReceived),
                    TotalContractorCost = g.Sum(x => x.JobContractors.Sum(c => (decimal)c.Pay)),
                })
                .OrderByDescending(r => r.TotalJobs)
                .ToListAsync();

            return query;
        }

    }
}
