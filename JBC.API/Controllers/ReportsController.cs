using JBC.Data;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IUnitOfWork _context;

    public ReportsController(IUnitOfWork context)
    {
        _context = context;
    }

    [HttpGet("partner-summary")]
    public async Task<ActionResult<IEnumerable<PartnerJobSummaryDto>>> GetPartnerSummary(DateOnly startDate, DateOnly endDate, bool combineNoPartner)
    {
        var query = await _context.Jobs.PartnerJobSummary(startDate, endDate, combineNoPartner);

        return Ok(query);
    }

    [HttpGet("contractors")]
    public async Task<ActionResult<IEnumerable<PartnerJobSummaryDto>>> GetContractorReport(DateOnly startDate, DateOnly endDate, bool combineNoPartner)
    {
        var query = await _context.Jobs.GetContractorReportAsync(startDate, endDate, combineNoPartner);

        return Ok(query);
    }

    [HttpGet("vans")]
    public async Task<IActionResult> GetVanReport(DateOnly startDate, DateOnly endDate)
        => Ok(await _context.Jobs.GetVanReportAsync(startDate, endDate));

    [HttpGet("jobs")]
    public async Task<IActionResult> GetJobSummaryReport(DateOnly startDate, DateOnly endDate)
        => Ok(await _context.Jobs.GetJobSummaryReportAsync(startDate, endDate));
}
