using JBC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JBC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats([FromQuery] DateOnly? start = null, [FromQuery] DateOnly? end = null)
        {
            var result = await _dashboardService.GetStatsAsync(start, end);
            return Ok(result);
        }

        [HttpGet("vans-stats")]
        public async Task<IActionResult> GetVansStats([FromQuery] DateOnly? start = null, [FromQuery] DateOnly? end = null)
        {
            var result = await _dashboardService.GetVansStatsAsync(start, end);
            return Ok(result);
        }

        [HttpGet("contractors-stats")]
        public async Task<IActionResult> GetContractorsStats([FromQuery] DateOnly? start = null, [FromQuery] DateOnly? end = null)
        {
            var result = await _dashboardService.GetContractorsStatsAsync(start, end);
            return Ok(result);
        }
    }
}
