using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobTypeController : ControllerBase
    {
        private readonly IJobTypeService _jobTypeService;

        public JobTypeController(IJobTypeService jobTypeService)
        {
            _jobTypeService = jobTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTypeDto>>> GetAll()
        {
            var jobTypes = await _jobTypeService.GetAllAsync();
            return Ok(jobTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobTypeDto>> Get(int id)
        {
            var jobTypeDto = await _jobTypeService.GetByIdAsync(id);
            if (jobTypeDto == null) return NotFound();
            return Ok(jobTypeDto);
        }

        [HttpPost]
        public async Task<ActionResult<JobTypeDto>> Create(JobTypeDto jobTypeDto)
        {
            var result = await _jobTypeService.CreateAsync(jobTypeDto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobTypeDto jobTypeDto)
        {
            await _jobTypeService.UpdateAsync(id, jobTypeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
