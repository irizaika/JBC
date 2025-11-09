using AutoMapper;
using JBC.Application.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public JobController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetAll()
        {
            var jobs = await _uow.Jobs.GetAllAsync();
            var jobDtos = _mapper.Map<IEnumerable<JobDto>>(jobs);
            return Ok(jobDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobDto>> GetJobById(int id)
        {
           // var job = await _uow.Jobs.GetByIdAsync(id);
            var job = await _uow.Jobs.GetJobsWithRelationsAsync(id);

            if (job == null) return NotFound();
            var jobDto = _mapper.Map<JobDto>(job);
            return Ok(jobDto);
        }

        //[HttpGet("range")]
        //public async Task<ActionResult<IEnumerable<JobDto>>> GetInRange(DateTime start, DateTime end)
        //{
        //    var temp = await _uow.Jobs.GetJobsInRangeAsync(start, end);
        //   // if (temp == null || !temp.Any()) return NotFound();
        //    var mapped = _mapper.Map<List<JobDto>>(temp);
        //    return  Ok(mapped);
        //}

        [HttpGet("range")]
        public async Task<ActionResult<IEnumerable<object>>> GetInRange(DateOnly start, DateOnly end)
        {
            // Fetch jobs in range
            var jobs = await _uow.Jobs.GetJobsInRangeAsync(start, end);
            var mappedJobs = _mapper.Map<List<JobDto>>(jobs);

            // Group jobs by date (assuming JobDto has a Date property)
            var jobsByDate = mappedJobs
                .GroupBy(j => j.Date)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Create a list covering every day in range
            var result = new List<object>();
            for (var date = start; date <= end; date = date.AddDays(1))
            {
                result.Add(new
                {
                    Date = date,
                    Jobs = jobsByDate.ContainsKey(date) ? jobsByDate[date] : new List<JobDto>()
                });
            }

            return Ok(result);
        }


        [HttpGet("day/{day}")]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetDay(DateOnly day)
        {
            var temp = await _uow.Jobs.GetDayJobsAsync(day);
          //  if (temp == null || !temp.Any()) return NotFound();
            var mapped = _mapper.Map<List<JobDto>>(temp);
            return Ok(mapped);
        }

        [HttpPost]
        public async Task<ActionResult<JobDto>> Create(JobDto jobDto)
        {

            var job = _mapper.Map<Job>(jobDto);

            await _uow.Jobs.AddAsync(job);
            await _uow.SaveAsync();

            var createdJobDto = _mapper.Map<JobDto>(job);

            return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, createdJobDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobDto jobDto)
        {
            //var updated = _mapper.Map<Job>(jobDto);
            //if (id != updated.Id) return BadRequest();

            ////var job = await _uow.Jobs.GetByIdAsync(id);
            //var job = await _uow.Jobs.GetJobsWithRelationsAsync(id);

            //_uow.Jobs.Update(job);
            //await _uow.SaveAsync();
            //return NoContent();

            if (id != jobDto.Id) return BadRequest("ID mismatch");

            // 1. Load existing job with relations
            var job = await _uow.Jobs.GetJobsWithRelationsAsync(id);
            if (job == null) return NotFound();

            // 2. Map updated simple fields from DTO → existing entity
            _mapper.Map(jobDto, job);

            // 3. Update Many-to-Many relationships manually

            // --- Contractors ---
            job.JobContractors.Clear();
            if (jobDto.Contractors != null)
            {
                job.JobContractors = jobDto.Contractors
                    .Select(c => new JobContractor { JobId = id, ContractorId = c.ContractorId, Pay = c.Pay})
                    .ToList();
            }

            // --- Vans ---
            job.JobVans.Clear();
            if (jobDto.Vans != null)
            {
                job.JobVans = jobDto.Vans
                    .Select(vId => new JobVan { JobId = id, VanId = vId })
                    .ToList();
            }

            await _uow.SaveAsync();


            var updatedJobDto = _mapper.Map<JobDto>(job);

            return Ok(updatedJobDto);
            //return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _uow.Jobs.GetByIdAsync(id);
            if (job == null) return NotFound();
            _uow.Jobs.Remove(job);
            await _uow.SaveAsync();
            return NoContent();
        }
    }
}
