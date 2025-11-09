using JBC.Domain.Enum;
using JBC.Infrastructure.Data;
using JBC.Application.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using JBC.Application.Services;
using JBC.Application.Intefraces.CrudInterfaces;

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



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Register UnitOfWork (scoped per request)
builder.Services.AddScoped<IJobRepository, JobRepository>();



//builder.Services.AddScoped<IVanService, VanService>();

//builder.Services.AddAutoMapper(typeof(Program)); // Scans all profiles in the assembly
builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Van, VanDto>().ReverseMap();
    config.CreateMap<Job, JobDto>().ReverseMap();

    //config.CreateMap<Contractor, ContractorDto>()
    //.ForMember(dest => dest.Status, opt =>
    //    opt.MapFrom(src => ((ContractorStatus)src.Status).ToString())) // int ? string
    //.ReverseMap()
    //.ForMember(dest => dest.Status, opt =>
    //    opt.MapFrom(src => (Enum.TryParse<ContractorStatus>(src.Status, out ContractorStatus status))
    //        ? (int)status
    //        : (int)ContractorStatus.NotSet)); // string ? int

    config.CreateMap<Contractor, ContractorDto>().ReverseMap();
           //.ForMember(dest => dest.Status,
           //    opt => opt.MapFrom(src => ContractorMappingHelpers.ParseStatus(src.Status)));

    config.CreateMap<Job, JobDto>()
                .ForMember(dest => dest.PartnerName,
                    opt => opt.MapFrom(src => src.Partner != null ? src.Partner.CompanyName : null))
                .ForMember(dest => dest.JobTypeName,
                    opt => opt.MapFrom(src => src.JobType != null ? src.JobType.Name : null))
                .ForMember(dest => dest.Contractors,
                    opt => opt.MapFrom(src => src.JobContractors != null
                        ? src.JobContractors.Select(jc => new JobContractorDto { ContractorId = jc.ContractorId, Pay = jc.Pay } /*, ContractorName = jc.Contractor.ShortName*/ ).ToList()
                        : Enumerable.Empty<JobContractorDto>()))
                .ForMember(dest => dest.Vans,
                    opt => opt.MapFrom(src => src.JobVans != null
                        ? src.JobVans.Select(jv => jv.VanId/*, VanName = jv.Van.VanName*/).ToList()
                        : Enumerable.Empty<int>()))
                .ForMember(dest => dest.CustomerName, 
                    opt => opt.MapFrom(src => string.IsNullOrEmpty(src.CustomerName) ? null : src.CustomerName))
                .ReverseMap()
                .ForMember(dest => dest.JobContractors, opt => opt.MapFrom(src =>
                    src.Contractors != null
                        ? src.Contractors.Select(c => new JobContractor { ContractorId = c.ContractorId, Pay=c.Pay, JobId = src.Id })
                        : new List<JobContractor>()))
                .ForMember(dest => dest.JobVans, opt => opt.MapFrom(src =>
                    src.Vans != null
                        ? src.Vans.Select(v => new JobVan { VanId = v, JobId = src.Id })
                        : new List<JobVan>()))
                .ForMember(dest => dest.Partner, opt => opt.Ignore())
                .ForMember(dest => dest.JobType, opt => opt.Ignore());

    config.CreateMap<Partner, PartnerDto>().ReverseMap();
    config.CreateMap<Role, RoleDto>().ReverseMap();
    config.CreateMap<JobCategory, JobCategoryDto>().ReverseMap();
    config.CreateMap<JobType, JobTypeDto>().ReverseMap();
    config.CreateMap<PartnersRate, PartnersRateDto>().ReverseMap();
    config.CreateMap<ContractorsRate, ContractorsRateDto>().ReverseMap();
    config.CreateMap<PersonPayRatePerJobType, PersonPayRatePerJobTypeDto>().ReverseMap();
    config.CreateMap<RolePayRatePerJobCategory, RolePayRatePerJobCategoryDto>().ReverseMap();
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React dev server
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

app.UseHttpsRedirection();

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

static int ParseStatus(string s) =>
    Enum.TryParse<ActivityStatus>(s, out var status)
        ? (int)status
        : (int)ActivityStatus.NotSet;

public static class ContractorMappingHelpers
{
    public static int ParseStatus(int status)
    {
        //if (Enum.TryParse<ContractorStatus>(statusString, out var status))
            return (int)status;
      //  return (int)ContractorStatus.NotSet;
    }
}