using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JBC.Infrastructure.Data
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


        public async Task<List<JobSummaryPeriodReportDto>> GetJobSummaryReportAsync(DateOnly startDate, DateOnly endDate, string period = "week")
        {
            var data = await _context.Jobs
                .Include(j => j.JobContractors)
                .Where(j => j.Date >= startDate && j.Date <= endDate)
                .ToListAsync();

            // Group logic based on period
            var lower = period.ToLower();

            IEnumerable<IGrouping<object, Job>> grouped;
            List<JobSummaryPeriodReportDto> result = new List<JobSummaryPeriodReportDto>();

            try
            {
                switch (lower)
                {
                    case "month":
                        grouped = data.GroupBy(j => new
                        {
                            j.Date.Year,
                            j.Date.Month
                        });
                        break;

                    case "week":
                        var calendar = CultureInfo.InvariantCulture.Calendar;

                        grouped = data.GroupBy(j => new
                        {
                            j.Date.Year,
                            Week = calendar.GetWeekOfYear(
                                j.Date.ToDateTime(TimeOnly.MinValue),
                                CalendarWeekRule.FirstFourDayWeek,
                                DayOfWeek.Monday
                            )
                        });
                        break;

                    default:
                        grouped = data.GroupBy(j => j.Date.ToString("yyyy-MM-dd"));
                        break;
                }


                result = grouped.Select(g => new JobSummaryPeriodReportDto
                {
                    Period = period,
                    Key = g.Key.ToString(),
                    TotalJobs = g.Count(),
                    TotalReceived = g.Sum(x => x.PayReceived),
                    TotalPaidToContractors = g.Sum(x => x.JobContractors.Sum(c => (decimal)c.Pay)),
                    Profit = (decimal)g.Sum(x => x.PayReceived) - g.Sum(x => x.JobContractors.Sum(c => (decimal)c.Pay))
                })
                .OrderByDescending(x => x.Key)
                .ToList();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public async Task<List<CalendarChartDto>> GetJobsCalendarAsync()
        {
            // Load jobs in range
            var jobs = await _context.Jobs
                // .Where(j => j.Date >= startDate && j.Date <= endDate)
                .ToListAsync();

            var startDate = jobs.Min(j => j.Date);
            var endDate = jobs.Max(j => j.Date);

            // Group by day
            var grouped = jobs
                .GroupBy(j => j.Date)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );

            var result = new List<CalendarChartDto>();

            // Fill missing days
            for (var d = startDate; d <= endDate; d = d.AddDays(1))
            {
                     if (grouped.ContainsKey(d))
                {
                 result.Add(new CalendarChartDto
                {
                  Day = d.ToString("yyyy-MM-dd"),
                    Value = grouped.ContainsKey(d) ? grouped[d] : 0
                });
        }
            }

            return result;
        }

        public async Task<List<CalendarChartDto>> GetJobsCalendarAsync(int? vanId = null, int? contractorId = null, int? partnerId = null, int? jobTypeId = null)
        {
            var jobsQuery = _context.Jobs
                .Include(j => j.JobVans)
                    .ThenInclude(jv => jv.Van)
                .Include(j => j.JobContractors)
                    .ThenInclude(jc => jc.Contractor)
                .AsQueryable();

            // Apply filters
            if (vanId.HasValue)
                jobsQuery = jobsQuery.Where(j => j.JobVans.Any(jv => jv.VanId == vanId.Value));

            if (contractorId.HasValue)
                jobsQuery = jobsQuery.Where(j => j.JobContractors.Any(jc => jc.ContractorId == contractorId.Value));

            if (partnerId.HasValue)
                jobsQuery = jobsQuery.Where(j => j.PartnerId == partnerId.Value);

            if (jobTypeId.HasValue)
                jobsQuery = jobsQuery.Where(j => j.JobTypeId == jobTypeId.Value);

            var jobs = await jobsQuery.ToListAsync();

            if (!jobs.Any()) return new List<CalendarChartDto>();

            var startDate = jobs.Min(j => j.Date);
            var endDate = jobs.Max(j => j.Date);

            var grouped = jobs
                .GroupBy(j => j.Date)
                .ToDictionary(g => g.Key, g => g.Count());

            var result = new List<CalendarChartDto>();

            for (var d = startDate; d <= endDate; d = d.AddDays(1))
            {
                if (grouped.ContainsKey(d))
                {
                    result.Add(new CalendarChartDto
                    {
                        Day = d.ToString("yyyy-MM-dd"),
                        Value = grouped.ContainsKey(d) ? grouped[d] : 0
                    });
                }
            }

            return result;
        }




        //public async Task<List<JobSummaryPeriodReportDto>> GetJobSummaryReportAsync(DateOnly startDate, DateOnly endDate, string period = "day")
        //{
        //    var data = await _context.Jobs
        //        .Include(j => j.JobContractors)
        //        .Where(j => j.Date >= startDate && j.Date <= endDate)
        //        .ToListAsync();

        //    var lower = period.ToLower();
        //    IEnumerable<IGrouping<object, Job>> grouped;

        //    // ====== GROUP EXISTING JOBS =======
        //    switch (lower)
        //    {
        //        case "month":
        //            grouped = data.GroupBy(j => new { j.Date.Year, j.Date.Month });
        //            break;

        //        case "week":
        //            grouped = data.GroupBy(j => new
        //            {
        //                j.Date.Year,
        //                Week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
        //                    j.Date.ToDateTime(TimeOnly.MinValue),
        //                    CalendarWeekRule.FirstFourDayWeek,
        //                    DayOfWeek.Monday)
        //            });
        //            break;

        //        default: // day
        //            grouped = data.GroupBy(j => j.Date.ToString("yyyy-MM-dd"));
        //            break;
        //    }

        //    // ====== BUILD COMPLETE PERIOD RANGE =======
        //    var result = new List<JobSummaryPeriodReportDto>();

        //    if (lower == "day")
        //    {
        //        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        //        {
        //            var g = grouped.FirstOrDefault(x => x.Key.Equals(date));
        //            result.Add(CreateDto(period, date.ToString("yyyy-MM-dd"), g));
        //        }
        //    }
        //    else if (lower == "month")
        //    {
        //        var cur = new DateTime(startDate.Year, startDate.Month, 1);
        //        var last = new DateTime(endDate.Year, endDate.Month, 1);

        //        for (; cur <= last; cur = cur.AddMonths(1))
        //        {
        //            var g = grouped.FirstOrDefault(x =>
        //                (int)x.Key.GetType().GetProperty("Year").GetValue(x.Key)! == cur.Year &&
        //                (int)x.Key.GetType().GetProperty("Month").GetValue(x.Key)! == cur.Month);

        //            result.Add(CreateDto(period, $"{cur:yyyy-MM}", g));
        //        }
        //    }
        //    else if (lower == "week")
        //    {
        //        var calendar = CultureInfo.InvariantCulture.Calendar;
        //        var startWeek = calendar.GetWeekOfYear(startDate.ToDateTime(TimeOnly.MinValue), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        //        var endWeek = calendar.GetWeekOfYear(endDate.ToDateTime(TimeOnly.MinValue), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        //        for (int y = startDate.Year; y <= endDate.Year; y++)
        //        {
        //            for (int w = (y == startDate.Year ? startWeek : 1); w <= (y == endDate.Year ? endWeek : 52); w++)
        //            {
        //                var g = grouped.FirstOrDefault(x =>
        //                    (int)x.Key.GetType().GetProperty("year").GetValue(x.Key)! == y &&
        //                    (int)x.Key.GetType().GetProperty("week").GetValue(x.Key)! == w);

        //                result.Add(CreateDto(period, $"{y}-W{w}", g));
        //            }
        //        }
        //    }

        //    return result.OrderBy(x => x.Key).ToList();
        //}


        //// ====== Helper to build DTO with correct 0 values when missing =======
        //private static JobSummaryPeriodReportDto CreateDto(string period, string key, IGrouping<object, Job>? g)
        //{
        //    return new JobSummaryPeriodReportDto
        //    {
        //        Period = period,
        //        Key = key,
        //        TotalJobs = g?.Count() ?? 0,
        //        TotalReceived = g?.Sum(x => x.PayReceived) ?? 0,
        //        TotalPaidToContractors = g?.Sum(x => x.JobContractors.Sum(c => (decimal)c.Pay)) ?? 0,
        //        Profit = (g?.Sum(x => x.PayReceived) ?? 0) -
        //                 (g?.Sum(x => x.JobContractors.Sum(c => (decimal)c.Pay)) ?? 0)
        //    };
        //}


        //// ===========================
        //// VAN REPORT
        //// ===========================
        //public async Task<List<VanReportDto>> GetVanReportAsync(DateOnly startDate, DateOnly endDate)
        //{
        //    var report = await _context.JobVans
        //        .Include(jv => jv.Job)
        //        .Include(jv => jv.Van)
        //        .Where(jv => jv.Job.Date >= startDate && jv.Job.Date <= endDate)
        //        .GroupBy(jv => new
        //        {
        //            jv.VanId,
        //            jv.Van.VanName
        //        })
        //        .Select(g => new VanReportDto
        //        {
        //            VanId = g.Key.VanId,
        //            VanName = g.Key.VanName,
        //            TotalJobs = g.Count(),
        //            FirstJobDate = g.Min(x => x.Job.Date.ToDateTime(TimeOnly.MinValue)),
        //            LastJobDate = g.Max(x => x.Job.Date.ToDateTime(TimeOnly.MinValue))
        //        })
        //        .OrderByDescending(r => r.TotalJobs)
        //        .ToListAsync();

        //    return report;
        //}

        //// ===========================
        //// JOB SUMMARY REPORT
        //// ===========================
        //public async Task<List<JobSummaryReportDto>> GetJobSummaryReportAsync(DateOnly startDate, DateOnly endDate)
        //{
        //    var report = await _context.Jobs
        //        .Include(j => j.JobContractors)
        //        .Where(j => j.Date >= startDate && j.Date <= endDate)
        //        .GroupBy(j => j.Date)
        //        .Select(g => new JobSummaryReportDto
        //        {
        //            Date = g.Key,
        //            TotalJobs = g.Count(),
        //            TotalReceived = g.Sum(x => x.PayReceived),
        //            TotalPaidToContractors = g.Sum(x => x.JobContractors.Sum(c => (decimal)c.Pay))
        //        })
        //        .OrderByDescending(x => x.Date)
        //        .ToListAsync();

        //    return report;
        //}


    }
}