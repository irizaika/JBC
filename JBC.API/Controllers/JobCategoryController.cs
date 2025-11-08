using AutoMapper;
using JBC.Data;
using JBC.Data.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public JobCategoryController(IUnitOfWork uow, IMapper mapper) 
        { 
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCategoryDto>>> GetAll()
        {
            var jobCategories = await _uow.JobCategories.GetAllAsync();
            var jobCategoryDtos = _mapper.Map<IEnumerable<JobCategoryDto>>(jobCategories);
            return Ok(jobCategoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobCategoryDto>> Get(int id)
        {
            var jobCategory = await _uow.JobCategories.GetByIdAsync(id);
            if (jobCategory == null) return NotFound();

            var jobCategoryDto = _mapper.Map<JobCategoryDto>(jobCategory);
            return Ok(jobCategoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<JobCategoryDto>> Create(JobCategoryDto jobCategoryDto)
        {
            var jobCategory = _mapper.Map<JobCategory>(jobCategoryDto);

            await _uow.JobCategories.AddAsync(jobCategory);
            await _uow.SaveAsync();

            var responceDto = _mapper.Map<JobCategoryDto>(jobCategory);

            return CreatedAtAction(nameof(Get), new { id = jobCategoryDto.Id }, responceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobCategoryDto jobCategoryDto)
        {
            if (id != jobCategoryDto.Id) return BadRequest();

            var jobCategory = _mapper.Map<JobCategory>(jobCategoryDto);

            _uow.JobCategories.Update(jobCategory);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobCategory = await _uow.JobCategories.GetByIdAsync(id);
            if (jobCategory == null) return NotFound();
            _uow.JobCategories.Remove(jobCategory);
            await _uow.SaveAsync();
            return NoContent();
        }
    }
}
