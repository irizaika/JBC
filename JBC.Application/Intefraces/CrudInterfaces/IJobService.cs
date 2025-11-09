using JBC.Application.Interfaces;
using JBC.Domain.Dto;

namespace JBC.Application.Interfaces.CrudInterfaces
{
    public interface IJobService : ICrudService<JobDto>
    {
        Task<JobDto> UpdateJobAsync(int id, JobDto dto);
        Task<IEnumerable<object>> GetJobsInRangeAsync(DateOnly start, DateOnly end);
        Task<IEnumerable<JobDto>> GetDayJobsAsync(DateOnly day);
    }
}
