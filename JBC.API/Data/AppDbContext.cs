using JBC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

namespace JBC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Van> Vans { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnersRate> PartnersRates { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobContractor> JobContractors { get; set; }
        public DbSet<JobVan> JobVans { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<ContractorsRate> ContractorRates { get; set; }
        public DbSet<RolePayRatePerJobCategory> RoleRatesPerJobCategory { get; set; }
        public DbSet<PersonPayRatePerJobType> ContractorRatesPErJobType { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // JobContractor - Define composite primary key
            modelBuilder.Entity<JobContractor>()
                .HasKey(jc => new { jc.JobId, jc.ContractorId });

            // Define relationships
            modelBuilder.Entity<JobContractor>()
                .HasOne(jc => jc.Job)
                .WithMany(j => j.JobContractors)
                .HasForeignKey(jc => jc.JobId);

            // JobVan - Composite key
            modelBuilder.Entity<JobVan>()
                .HasKey(jv => new { jv.JobId, jv.VanId });

            modelBuilder.Entity<JobVan>()
                .HasOne(jv => jv.Job)
                .WithMany(j => j.JobVans)
                .HasForeignKey(jv => jv.JobId);

        }
    }
}
