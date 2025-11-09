using AutoMapper;
using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Application.Interfaces;
using JBC.Domain.Dto;
using JBC.Domain.Entities;

namespace JBC.Application.Services
{
    public class JobCategoryService : CrudService<JobCategoryDto, JobCategory>, IJobCategoryService
    {
        public JobCategoryService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper, uow.JobCategories)
        {
        }
    }
}
