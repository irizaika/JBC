using JBC.Application.Interfaces;
using JBC.Domain.Entities;

namespace JBC.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Contractor> Contractors { get; }
        public IGenericRepository<Van> Vans { get; }
        public IGenericRepository<Partner> Partners { get; }
        public IJobRepository Jobs { get; }
        public IGenericRepository<JobCategory> JobCategories { get; }
        public IGenericRepository<JobType> JobTypes { get; }
        public IGenericRepository<Role> Roles { get; }
        public IGenericRepository<ContractorsRate> ContractorRates { get; }
        public IGenericRepository<PartnersRate> PartnersRates { get; }
        public IGenericRepository<PersonPayRatePerJobType> PersonRatesPerJobType { get; }
        public IGenericRepository<RolePayRatePerJobCategory> RoleRatePerJobCategory { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Contractors = new GenericRepository<Contractor>(_context);
            Vans = new GenericRepository<Van>(_context);
            Partners = new GenericRepository<Partner>(_context);
            Jobs = new JobRepository(_context);
            JobTypes = new GenericRepository<JobType>(_context);
            Roles = new GenericRepository<Role>(_context);
            ContractorRates = new GenericRepository<ContractorsRate>(_context);
            PartnersRates = new GenericRepository<PartnersRate>(_context);
            JobCategories = new GenericRepository<JobCategory>(_context);
            PersonRatesPerJobType = new GenericRepository<PersonPayRatePerJobType>(_context);
            RoleRatePerJobCategory = new GenericRepository<RolePayRatePerJobCategory>(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
