using JBC.Data;
using JBC.Data.Interfaces;
using JBC.Models.Dto;
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
    public async Task<ActionResult<IEnumerable<PartnerJobSummaryDto>>> GetPartnerSummary(DateOnly startDate, DateOnly endDate)
    {
        var query = await _context.Jobs.PartnerJobSummary(startDate, endDate);

        return Ok(query);
    }
}
