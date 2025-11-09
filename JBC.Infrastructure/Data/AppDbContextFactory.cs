using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace JBC.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Build config to read from appsettings.json
            var settingsPath = Environment.GetEnvironmentVariable("JBC_APPSETTINGS_PATH")
                              ?? Path.Combine(Directory.GetCurrentDirectory(), "../JBC.API/appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonFile(settingsPath, optional: false)
                .Build();


            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
