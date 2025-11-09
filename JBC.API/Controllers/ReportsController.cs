using Microsoft.AspNetCore.Mvc;
using JBC.Application.Interfaces;

namespace JBC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("partner-summary")]
        public async Task<IActionResult> GetPartnerSummary(DateOnly startDate, DateOnly endDate, bool combineNoPartner)
            => Ok(await _reportService.GetPartnerSummaryAsync(startDate, endDate, combineNoPartner));

        [HttpGet("contractors")]
        public async Task<IActionResult> GetContractorReport(DateOnly startDate, DateOnly endDate, bool combineNoPartner)
            => Ok(await _reportService.GetContractorReportAsync(startDate, endDate, combineNoPartner));

        //[HttpGet("vans")]
        //public async Task<IActionResult> GetVanReport(DateOnly startDate, DateOnly endDate)
        //    => Ok(await _context.Jobs.GetVanReportAsync(startDate, endDate));

        //[HttpGet("jobs")]
        //public async Task<IActionResult> GetJobSummaryReport(DateOnly startDate, DateOnly endDate)
        //    => Ok(await _context.Jobs.GetJobSummaryReportAsync(startDate, endDate));
    }
}
