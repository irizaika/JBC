using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobCategoryController : ControllerBase
    {
        private readonly IJobCategoryService _jobCategoryService;

        public JobCategoryController(IJobCategoryService jobCategoryService)
        {
            _jobCategoryService = jobCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCategoryDto>>> GetAll()
        {
            var jobCategories = await _jobCategoryService.GetAllAsync();
            return Ok(jobCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobCategoryDto>> Get(int id)
        {
            var jobCategoryDto = await _jobCategoryService.GetByIdAsync(id);
            if (jobCategoryDto == null) return NotFound();
            return Ok(jobCategoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<JobCategoryDto>> Create(JobCategoryDto jobCategoryDto)
        {
            var result = await _jobCategoryService.CreateAsync(jobCategoryDto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobCategoryDto jobCategoryDto)
        {
            await _jobCategoryService.UpdateAsync(id, jobCategoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobCategoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
