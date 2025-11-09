using JBC.Application.Interfaces;
using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _jobService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _jobService.GetByIdAsync(id);
            return job == null ? NotFound() : Ok(job);
        }

        [HttpGet("range")]
        public async Task<IActionResult> GetRange(DateOnly start, DateOnly end)
            => Ok(await _jobService.GetJobsInRangeAsync(start, end));

        [HttpGet("day/{day}")]
        public async Task<IActionResult> GetDay(DateOnly day)
            => Ok(await _jobService.GetDayJobsAsync(day));

        [HttpPost]
        public async Task<IActionResult> Create(JobDto jobDto)
        {
            var created = await _jobService.CreateAsync(jobDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobDto jobDto)
        {
            var updated = await _jobService.UpdateJobAsync(id, jobDto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobService.DeleteAsync(id);
            return NoContent();
        }
    }
}
