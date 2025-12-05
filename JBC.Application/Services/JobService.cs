using JBC.Application.Interfaces;
using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JBC.Application.Services
{
    public class JobService : CrudService<JobDto, Job>, IJobService
    {
        public JobService(IUnitOfWork uow, IMapper<Job, JobDto> mapper) : base(uow, mapper, uow.Jobs)
        {
        }

        public new async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            var jobs = await _uow.Jobs.GetAllAsync();
            return jobs.Select(e => _mapper.ToDto(e));
        }

        public new async Task<JobDto?> GetByIdAsync(int id)
        {
            var job = await _uow.Jobs.GetJobsWithRelationsAsync(id);
            return job == null ? null : _mapper.ToDto(job);
        }

        public new async Task<JobDto> CreateAsync(JobDto dto)
        {
            var job = _mapper.ToEntity(dto);
            await _uow.Jobs.AddAsync(job);
            await _uow.SaveAsync();
            return _mapper.ToDto(job);
        }

        public async Task<JobDto> UpdateJobAsync(int id, JobDto dto)
        {
            if (id != dto.Id)
                throw new ArgumentException("ID mismatch");

            var job = await _uow.Jobs.GetJobsWithRelationsAsync(id);
            if (job == null)
                throw new KeyNotFoundException("Job not found");

            var updatedJob = _mapper.ToEntity(dto);

            job.PartnerId = updatedJob.PartnerId;
            job.Date = updatedJob.Date;
            job.CustomerName = updatedJob.CustomerName;
            job.PayReceived = updatedJob.PayReceived;
            job.PartnerId = updatedJob.PartnerId;
            job.JobTypeId = updatedJob.JobTypeId;
            job.Count = updatedJob.Count;
            job.Details = updatedJob.Details;


            job.JobContractors.Clear();
            if (dto.Contractors != null)
            {
                job.JobContractors = dto.Contractors
                    .Select(c => new JobContractor
                    {
                        JobId = id,
                        ContractorId = c.ContractorId,
                        Pay = c.Pay
                    })
                    .ToList();
            }

            job.JobVans.Clear();
            if (dto.Vans != null)
            {
                job.JobVans = dto.Vans
                    .Select(vId => new JobVan
                    {
                        JobId = id,
                        VanId = vId
                    })
                    .ToList();
            }

            await _uow.SaveAsync();
            return _mapper.ToDto(job);
        }

        public new async Task DeleteAsync(int id)
        {
            var job = await _uow.Jobs.GetByIdAsync(id);
            if (job == null)
                throw new KeyNotFoundException("Job not found");

            _uow.Jobs.Remove(job);
            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<object>> GetJobsInRangeAsync(DateOnly start, DateOnly end)
        {
            var jobs = await _uow.Jobs.GetJobsInRangeAsync(start, end);
            var mappedJobs = jobs.Select(e => _mapper.ToDto(e));

            var jobsByDate = mappedJobs
                .GroupBy(j => j.Date)
                .ToDictionary(g => g.Key, g => g.ToList());

            var result = new List<object>();
            for (var date = start; date <= end; date = date.AddDays(1))
            {
                result.Add(new
                {
                    Date = date,
                    Jobs = jobsByDate.ContainsKey(date) ? jobsByDate[date] : new List<JobDto>()
                });
            }

            return result;
        }

        public async Task<IEnumerable<JobDto>> GetDayJobsAsync(DateOnly day)
        {
            var jobs = await _uow.Jobs.GetDayJobsAsync(day);
            return jobs.Select(e => _mapper.ToDto(e));
        }
    }
}
