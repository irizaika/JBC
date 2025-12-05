using JBC.Application.Interfaces;
using JBC.Application.Interfaces.CrudInterfaces;
using JBC.Application.Mappers;
using JBC.Application.Services;
using JBC.Domain.Dto;
using JBC.Domain.Entities;
using JBC.Domain.Enum;
using JBC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and services
builder.Services.AddScoped<IGenericRepository<Contractor>, GenericRepository<Contractor>>();
builder.Services.AddScoped<IContractorService, ContractorService>();

builder.Services.AddScoped<IGenericRepository<PersonPayRatePerJobType>, GenericRepository<PersonPayRatePerJobType>>();
builder.Services.AddScoped<IContractorPayService, ContractorPayService>();

builder.Services.AddScoped<IGenericRepository<JobType>, GenericRepository<JobType>>();
builder.Services.AddScoped<IJobTypeService, JobTypeService>();

builder.Services.AddScoped<IGenericRepository<JobCategory>, GenericRepository<JobCategory>>();
builder.Services.AddScoped<IJobCategoryService, JobCategoryService>();

builder.Services.AddScoped<IGenericRepository<Partner>, GenericRepository<Partner>>();
builder.Services.AddScoped<IPartnerService, PartnerService>();

builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IGenericRepository<RolePayRatePerJobCategory>, GenericRepository<RolePayRatePerJobCategory>>();
builder.Services.AddScoped<IRolePayService, RolePayService>();

builder.Services.AddScoped<IGenericRepository<Van>, GenericRepository<Van>>();
builder.Services.AddScoped<IVanService, VanService>();

builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Register UnitOfWork (scoped per request)
builder.Services.AddScoped<IJobRepository, JobRepository>();

builder.Services.AddScoped<IMapper<Contractor, ContractorDto>, ContractorMapping>();
builder.Services.AddScoped<IMapper<Job, JobDto>, JobMapping>();
builder.Services.AddScoped<IMapper<JobCategory, JobCategoryDto>, JobCategoryMapping>();
builder.Services.AddScoped<IMapper<JobType, JobTypeDto>, JobTypeMapping>();
builder.Services.AddScoped<IMapper<Partner, PartnerDto>, PartnerMapping>();
builder.Services.AddScoped<IMapper<PersonPayRatePerJobType, PersonPayRatePerJobTypeDto>, PersonPayRateMapping>();
builder.Services.AddScoped<IMapper<Role, RoleDto>, RoleMapping>();
builder.Services.AddScoped<IMapper<RolePayRatePerJobCategory, RolePayRatePerJobCategoryDto>, RolePayRateMapping>();
builder.Services.AddScoped<IMapper<Van, VanDto>, VanMapping>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend", policy =>
//    {
//        //var allowedOrigins = new List<string>
//        //{
//        //    "http://localhost:3001",
//        //    "https://localhost:3001",
//        //    "http://localhost:3000",
//        //    "https://localhost:3000",
//        //    "http://192.168.8.234:3001",
//        //    "https://192.168.8.234:3001"
//        //};

//        //// Read dynamic URL from environment variable
//        //var tunnelUrl = Environment.GetEnvironmentVariable("FRONTEND_TUNNEL_URL");
//        //if (!string.IsNullOrEmpty(tunnelUrl))
//        //{
//        //    allowedOrigins.Add(tunnelUrl);
//        //    Console.WriteLine($"Added dynamic CORS origin: {tunnelUrl}");
//        //}

//        //policy.WithOrigins(allowedOrigins.ToArray())
//        //      .AllowAnyHeader()
//        //      .AllowAnyMethod();
//        policy.AllowAnyOrigin()
//      .AllowAnyHeader()
//      .AllowAnyMethod();
//    });
//});


var frontendTunnel = Environment.GetEnvironmentVariable("FRONTEND_TUNNEL_URL");
var backendTunnel = Environment.GetEnvironmentVariable("BACKEND_TUNNEL_URL");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        var allowed = new[]
        {
            "http://localhost:3000",
            "http://localhost:3001",
            "https://localhost:3000",
            "https://localhost:3001",
            "https://80.89.73.59:3001",
            "http://80.89.73.59:3001",
            frontendTunnel
        };

        policy.WithOrigins(allowed.Where(x => !string.IsNullOrEmpty(x)).ToArray())
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();
ApplyMigration();

app.Run();


void ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (_db.Database.GetPendingMigrations().Count() > 0)
    {
        _db.Database.Migrate();
    }
}

//static int ParseStatus(string s) =>
//    Enum.TryParse<ActivityStatus>(s, out var status)
//        ? (int)status
//        : (int)ActivityStatus.NotSet;

//public static class ContractorMappingHelpers
//{
//    public static int ParseStatus(int status)
//    {
//        //if (Enum.TryParse<ContractorStatus>(statusString, out var status))
//            return (int)status;
//      //  return (int)ContractorStatus.NotSet;
//    }
//}