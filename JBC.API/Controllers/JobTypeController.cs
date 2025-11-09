using AutoMapper;
using JBC.Application.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobTypeController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public JobTypeController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTypeDto>>> GetAll()
        {
            var jobTypes = await _uow.JobTypes.GetAllAsync();

            var jobTypeDtos = _mapper.Map<IEnumerable<JobTypeDto>>(jobTypes);

            return Ok(jobTypeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobTypeDto>> Get(int id)
        {
            var jobType = await _uow.JobTypes.GetByIdAsync(id);

            if (jobType == null) return NotFound();

            var jobTypeDto = _mapper.Map<JobTypeDto>(jobType);

            return Ok(jobTypeDto);
        }

        [HttpPost]
        public async Task<ActionResult<JobTypeDto>> Create(JobTypeDto jobTypeDto)
        {
            if (jobTypeDto.PartnerId == 0)
                jobTypeDto.PartnerId = null;  //convert to null before mapping

            var jobType = _mapper.Map<JobType>(jobTypeDto);

            await _uow.JobTypes.AddAsync(jobType);
            await _uow.SaveAsync();

            var responceDto = _mapper.Map<JobTypeDto>(jobType);

            return CreatedAtAction(nameof(Get), new { id = jobType.Id }, responceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobTypeDto jobTypeDto)
        {
            if (id != jobTypeDto.Id) return BadRequest();

            if (jobTypeDto.PartnerId == 0)
                jobTypeDto.PartnerId = null;  //convert to null before mapping

            var jobType = _mapper.Map<JobType>(jobTypeDto);

            _uow.JobTypes.Update(jobType);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobType = await _uow.JobTypes.GetByIdAsync(id);

            if (jobType == null) return NotFound();

            _uow.JobTypes.Remove(jobType);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}
