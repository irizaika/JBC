using JBC.Data.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
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
            catch (Exception e)
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

        public async Task<List<PartnerJobSummaryDto>> PartnerJobSummary(DateOnly startDate, DateOnly endDate, bool combineNoPartner)
        {
            if (combineNoPartner == false)
            {
                var query = await _context.Jobs
                    .Where(j => j.Date >= startDate && j.Date <= endDate)
                    .GroupBy(j => new
                    {
                        j.PartnerId,
                        PartnerName = j.Partner != null ? j.Partner.CompanyName : null,
                        j.CustomerName
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
                        TotalJobs = (double)g.Sum(x => x.Count),
                        TotalPayReceived = g.Sum(x => x.PayReceived),
                        TotalContractorCost = g.Sum(x => x.JobContractors.Sum(c => (decimal)c.Pay)),
                    })
                    .OrderByDescending(r => r.TotalJobs)
                    .ToListAsync();

                return query;
            }
            else
            {
                var query = await _context.Jobs
                    .Where(j => j.Date >= startDate && j.Date <= endDate)
                    .GroupBy(j => new
                    {
                        j.PartnerId,
                        PartnerName = j.Partner != null ? j.Partner.CompanyName : null,
                    })
                    .Select(g => new PartnerJobSummaryDto
                    {
                        PartnerId = g.Key.PartnerId ?? 0,
                        PartnerName = g.Key.PartnerId == null ? "All custom jobs" : g.Key.PartnerName,
                        TotalJobs = (double)g.Sum(x => x.Count),
                        TotalPayReceived = g.Sum(x => x.PayReceived),
                        TotalContractorCost = g.Sum(x => x.JobContractors.Sum(c => (decimal)c.Pay)),
                    })
                    .OrderByDescending(r => r.TotalJobs)
                    .ToListAsync();

                return query;
            }
        }



        // ===========================
        // CONTRACTOR REPORT
        // ===========================
        public async Task<List<ContractorReportDto>> GetContractorReportAsync(DateOnly startDate, DateOnly endDate, bool combineNoPartner)
        {
            var baseQuery = await _context.JobContractors
                .Include(jc => jc.Job)
                    .ThenInclude(j => j.Partner)
                .Include(jc => jc.Contractor)
                .Where(jc => jc.Job.Date >= startDate && jc.Job.Date <= endDate)
                .ToListAsync(); // Force evaluation in memory (no APPLY translation)

            if (combineNoPartner == false)
            {
                var report = baseQuery
                .GroupBy(jc => new
                {
                    jc.ContractorId,
                    ContractorName =/*jc.Contractor.ShortName ??*/ jc.Contractor.Name
                })
                .Select(g => new ContractorReportDto
                {
                    ContractorId = g.Key.ContractorId,
                    ContractorName = g.Key.ContractorName,
                    TotalJobs = g.Count(),
                    TotalPay = g.Sum(x => (decimal)x.Pay),
                    AveragePayPerJob = g.Average(x => (decimal)x.Pay),

                    // Safe to group in memory
                    partnerJobList = g
                        .GroupBy(x => x.Job.Partner != null ? x.Job.Partner.CompanyName
                                                           : !string.IsNullOrEmpty(x.Job.CustomerName)
                                                               ? x.Job.CustomerName
                                                               : "Unassigned")
                        .Select(pg => new JobPerPartner
                        {
                            Name = pg.Key,
                            count = pg.Count()
                        })
                        .ToList()
                })
                .OrderByDescending(r => r.TotalJobs)
                .ToList();

                return report;
            }
            else
            {
                var report = baseQuery
                    .GroupBy(jc => new
                    {
                        jc.ContractorId,
                        ContractorName =/* jc.Contractor.ShortName ?? */jc.Contractor.Name
                    })
                    .Select(g => new ContractorReportDto
                    {
                        ContractorId = g.Key.ContractorId,
                        ContractorName = g.Key.ContractorName,
                        TotalJobs = g.Count(),
                        TotalPay = g.Sum(x => (decimal)x.Pay),
                        AveragePayPerJob = g.Average(x => (decimal)x.Pay),

                        // Safe to group in memory
                        partnerJobList = g
                            .GroupBy(x => x.Job.Partner != null ? x.Job.Partner.CompanyName : "All custom jobs")
                            .Select(pg => new JobPerPartner
                            {
                                Name = pg.Key,
                                count = pg.Count()
                            })
                            .ToList()
                    })
                    .OrderByDescending(r => r.TotalJobs)
                    .ToList();

                return report;
            }
        }


        // ===========================
        // VAN REPORT
        // ===========================
        public async Task<List<VanReportDto>> GetVanReportAsync(DateOnly startDate, DateOnly endDate)
        {
            var report = await _context.JobVans
                .Include(jv => jv.Job)
                .Include(jv => jv.Van)
                .Where(jv => jv.Job.Date >= startDate && jv.Job.Date <= endDate)
                .GroupBy(jv => new
                {
                    jv.VanId,
                    jv.Van.VanName
                })
                .Select(g => new VanReportDto
                {
                    VanId = g.Key.VanId,
                    VanName = g.Key.VanName,
                    TotalJobs = g.Count(),
                    FirstJobDate = g.Min(x => x.Job.Date.ToDateTime(TimeOnly.MinValue)),
                    LastJobDate = g.Max(x => x.Job.Date.ToDateTime(TimeOnly.MinValue))
                })
                .OrderByDescending(r => r.TotalJobs)
                .ToListAsync();

            return report;
        }

        // ===========================
        // JOB SUMMARY REPORT
        // ===========================
        public async Task<List<JobSummaryReportDto>> GetJobSummaryReportAsync(DateOnly startDate, DateOnly endDate)
        {
            var report = await _context.Jobs
                .Include(j => j.JobContractors)
                .Where(j => j.Date >= startDate && j.Date <= endDate)
                .GroupBy(j => j.Date)
                .Select(g => new JobSummaryReportDto
                {
                    Date = g.Key,
                    TotalJobs = g.Count(),
                    TotalReceived = g.Sum(x => x.PayReceived),
                    TotalPaidToContractors = g.Sum(x => x.JobContractors.Sum(c => (decimal)c.Pay))
                })
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return report;
        }


    }
}