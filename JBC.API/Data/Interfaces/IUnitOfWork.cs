using JBC.Domain.Entities;

namespace JBC.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Contractor> Contractors { get; }
        IGenericRepository<Van> Vans { get; }
        IGenericRepository<Partner> Partners { get; }
        IJobRepository Jobs { get; }
        IGenericRepository<JobCategory> JobCategories { get; }
        IGenericRepository<JobType> JobTypes { get; }
        IGenericRepository<Role> Roles { get; }
        IGenericRepository<ContractorsRate> ContractorRates { get; }
        IGenericRepository<PartnersRate> PartnersRates { get; }
        IGenericRepository<PersonPayRatePerJobType> PersonRatesPerJobType { get; }
        IGenericRepository<RolePayRatePerJobCategory> RoleRatePerJobCategory { get; }

        Task<int> SaveAsync();
    }
}
