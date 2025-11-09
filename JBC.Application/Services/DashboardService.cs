using JBC.Application.Interfaces;

namespace JBC.Application.Services
{

    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _uow;

        public DashboardService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<object> GetStatsAsync(DateOnly? start, DateOnly? end)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var firstDay = start ?? new DateOnly(today.Year, today.Month, 1);
            var lastDay = end ?? firstDay.AddMonths(1).AddDays(-1);
            int totalDays = (lastDay.DayNumber - firstDay.DayNumber) + 1;

            var jobs = await _uow.Jobs.GetJobsInRangeAsync(firstDay, lastDay);

            //// VAN Stats
            //var vanDays = jobs
            //    .SelectMany(j => j.JobVans.Select(v => new { v.VanId, j.Date }))
            //    .GroupBy(x => x.VanId)
            //    .ToDictionary(g => g.Key, g => g.Select(x => x.Date).Distinct().Count());

            //int totalVans = await _uow.Vans.GetCountAsync();
            //int totalVanDaysWorked = vanDays.Values.Sum();
            //int totalVanDaysPossible = totalVans * totalDays;
            //double vanWorkPercent = totalVanDaysPossible > 0 ? (double)totalVanDaysWorked / totalVanDaysPossible : 0;

            //// CONTRACTOR Stats
            //var contractorDays = jobs
            //    .SelectMany(j => j.JobContractors.Select(c => new { c.ContractorId, j.Date }))
            //    .GroupBy(x => x.ContractorId)
            //    .ToDictionary(g => g.Key, g => g.Select(x => x.Date).Distinct().Count());

            //int totalContractors = await _uow.Contractors.GetCountAsync();
            //int totalContractorDaysWorked = contractorDays.Values.Sum();
            //int totalContractorDaysPossible = totalContractors * totalDays;
            //double contractorWorkPercent = totalContractorDaysPossible > 0 ? (double)totalContractorDaysWorked / totalContractorDaysPossible : 0;

            // PROFIT
            decimal totalRevenue = jobs.Sum(j => j.PayReceived);
            decimal totalContractorCost = jobs.Sum(j => j.JobContractors.Sum(c => (decimal)c.Pay));
            decimal profit = totalRevenue - totalContractorCost;

            return new
            {
                startDate = firstDay,
                endDate = lastDay,
                //vans = new
                //{
                //    workPercent = vanWorkPercent,
                //    workedDays = totalVanDaysWorked,
                //    totalDays = totalDays
                //},
                //contractors = new
                //{
                //    workPercent = contractorWorkPercent,
                //    workedDays = totalContractorDaysWorked,
                //    totalDays = totalDays
                //},
                profit = new
                {
                    totalRevenue,
                    totalContractorCost,
                    profit
                }
            };
        }

        public async Task<IEnumerable<object>> GetVansStatsAsync(DateOnly? start, DateOnly? end)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var firstDay = start ?? new DateOnly(today.Year, today.Month, 1);
            var lastDay = end ?? firstDay.AddMonths(1).AddDays(-1);
            int totalDays = (lastDay.DayNumber - firstDay.DayNumber) + 1;

            var jobs = await _uow.Jobs.GetJobsInRangeAsync(firstDay, lastDay);
            var vans = await _uow.Vans.GetAllAsync();

            return vans.Select(v =>
            {
                var workedDays = jobs
                    .Where(j => j.JobVans.Any(jv => jv.VanId == v.Id))
                    .Select(j => j.Date)
                    .Distinct()
                    .Count();

                double percent = totalDays > 0 ? (double)workedDays / totalDays : 0;

                return new
                {
                    v.Id,
                    v.VanName,
                    v.Plate,
                    workedDays,
                    totalDays,
                    workPercent = percent
                };
            }).ToList();
        }

        public async Task<IEnumerable<object>> GetContractorsStatsAsync(DateOnly? start, DateOnly? end)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var firstDay = start ?? new DateOnly(today.Year, today.Month, 1);
            var lastDay = end ?? firstDay.AddMonths(1).AddDays(-1);
            int totalDays = (lastDay.DayNumber - firstDay.DayNumber) + 1;

            var jobs = await _uow.Jobs.GetJobsInRangeAsync(firstDay, lastDay);
            var contractors = await _uow.Contractors.GetAllAsync();

            return contractors.Select(c =>
            {
                var workedDays = jobs
                    .Where(j => j.JobContractors.Any(jc => jc.ContractorId == c.Id))
                    .Select(j => j.Date)
                    .Distinct()
                    .Count();

                double percent = totalDays > 0 ? (double)workedDays / totalDays : 0;

                return new
                {
                    c.Id,
                    c.Name,
                    workedDays,
                    totalDays,
                    workPercent = percent
                };
            }).ToList();
        }
    }
}
