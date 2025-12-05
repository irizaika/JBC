using JBC.Domain.Dto;
using JBC.Domain.Entities;
using JBC.Domain.Enum;

namespace JBC.Application.Helpers
{
    public static class Mapper
    {
        // Job ↔ JobDto
        //public static JobDto ToDto(this Job job)
        //{
        //    return new JobDto
        //    {
        //        Id = job.Id,
        //        Date = job.Date,
        //        CustomerName = job.CustomerName,
        //        PayReceived = job.PayReceived,
        //        PartnerId = job.PartnerId,
        //        PartnerName = job.Partner?.CompanyName,
        //        JobTypeId = job.JobTypeId,
        //        JobTypeName = job.JobType?.Name,
        //        Count =job.Count,
        //        Details = job.Details,
        //        Contractors = job.JobContractors?
        //            .Select(jc => new JobContractorDto
        //            {
        //                ContractorId = jc.ContractorId,
        //                Pay = jc.Pay
        //            }).ToList() ?? new List<JobContractorDto>(),
        //        Vans = job.JobVans?
        //            .Select(jv => jv.VanId)
        //            .ToList() ?? new List<int>()
        //    };
        //}

        //public static Job ToEntity(this JobDto dto)
        //{
        //    var job = new Job
        //    {
        //        Id = dto.Id,
        //        Date = dto.Date,
        //        CustomerName = dto.CustomerName,
        //        PayReceived = dto.PayReceived,
        //        PartnerId = dto.PartnerId,
        //        JobTypeId = dto.JobTypeId,
        //        Count = dto.Count,
        //        Details = dto.Details,
        //        JobContractors = dto.Contractors?.Select(c => new JobContractor
        //        {
        //            ContractorId = c.ContractorId,
        //            Pay = c.Pay,
        //            JobId = dto.Id
        //        }).ToList() ?? new List<JobContractor>(),
        //        JobVans = dto.Vans?.Select(v => new JobVan
        //        {
        //            VanId = v,
        //            JobId = dto.Id
        //        }).ToList() ?? new List<JobVan>()
        //    };

        //    return job;
        //}

        //// Contractor ↔ ContractorDto
        //public static ContractorDto ToDto(this Contractor c) => new ContractorDto
        //{
        //    Id = c.Id,
        //    Name = c.Name,
        //    Status = c.Status,
        //    Phone = c.Phone,
        //    Email = c.Email,
        //    ShortName = c.ShortName,
        //    Address = c.Address,
        //    BankAccount = c.BankAccount,
        //    RoleId = c.RoleId,
        //    DayRate = c.DayRate
        //};

        //public static Contractor ToEntity(this ContractorDto dto) => new Contractor
        //{
        //    Id = dto.Id,
        //    Name = dto.Name,
        //    Status = dto.Status,
        //    Phone = dto.Phone,
        //    Email = dto.Email,
        //    ShortName = dto.ShortName,
        //    Address = dto.Address,
        //    BankAccount = dto.BankAccount,
        //    RoleId = dto.RoleId,
        //    DayRate = dto.DayRate
        //};

        //// Van ↔ VanDto
        //public static VanDto ToDto(this Van v) => new VanDto
        //{
        //    Id = v.Id,
        //    VanName = v.VanName,
        //    Plate = v.Plate,
        //    Details = v.Details,
        //    Status = (int)v.Status //todo
        //};

        //public static Van ToEntity(this VanDto dto) => new Van
        //{
        //    Id = dto.Id,
        //    VanName = dto.VanName,
        //    Plate = dto.Plate,
        //    Details = dto.Details,
        //    Status = (ActivityStatus)dto.Status //todo
        //};

        // Partner ↔ PartnerDto
        //public static PartnerDto ToDto(this Partner p) => new PartnerDto
        //{
        //    Id = p.Id,
        //    CompanyName = p.CompanyName,
        //    ShortName = p.ShortName,
        //    Address = p.Address,
        //    Email = p.Email,
        //    Phone1 = p.Phone1,
        //    Phone2 = p.Phone2,
        //    Status = (int)p.Status
        //};

        //public static Partner ToEntity(this PartnerDto dto) => new Partner
        //{
        //    Id = dto.Id,
        //    CompanyName = dto.CompanyName,
        //    Address = dto.Address,
        //    ShortName = dto.ShortName,
        //    Email = dto.Email,
        //    Phone1 = dto.Phone1,
        //    Phone2 = dto.Phone2,
        //    Status = (ActivityStatus)dto.Status
        //};


        //public static RoleDto ToDto(this Role role) => new RoleDto
        //{
        //    Id = role.Id,
        //    RoleName = role.RoleName,
        //};

        //public static Role ToEntity(this RoleDto dto) => new Role
        //{
        //    Id = dto.Id,
        //    RoleName = dto.RoleName,
        //};


        //public static JobCategoryDto ToDto(this JobCategory jobCategory) => new JobCategoryDto
        //{
        //    Id = jobCategory.Id,
        //    Name = jobCategory.Name,
        //    Hours = jobCategory.Hours,
        //    IsCustom = jobCategory.IsCustom
        //};

        //public static JobCategory ToEntity(this JobCategoryDto dto) => new JobCategory
        //{
        //    Id = dto.Id,
        //    Name = dto.Name,
        //    Hours = dto.Hours,
        //    IsCustom = dto.IsCustom
        //};


        //public static JobTypeDto ToDto(this JobType jobType) => new JobTypeDto
        //{
        //    Id = jobType.Id,
        //    Description = jobType.Description,
        //    JobCategoryId = jobType.JobCategoryId,
        //    Name = jobType.Name,
        //    NumberOfPeople = jobType.NumberOfPeople,
        //    NumberOfVans = jobType.NumberOfVans,
        //    PartnerId = jobType.PartnerId,
        //    PayRate = jobType.PayRate
        //};

        //public static JobType ToEntity(this JobTypeDto dto) => new JobType
        //{
        //    Id = dto.Id,
        //    Description = dto.Description,
        //    JobCategoryId = dto.JobCategoryId,
        //    Name = dto.Name,
        //    NumberOfPeople = dto.NumberOfPeople,
        //    NumberOfVans = dto.NumberOfVans,
        //    PartnerId = dto.PartnerId,
        //    PayRate = dto.PayRate
        //};


        public static PartnersRateDto ToDto(this PartnersRate rate) => new PartnersRateDto
        {
            Id = rate.Id,
            JobTypeId = rate.JobTypeId,
            PartnerId = rate.PartnerId,
            Pay = rate.Pay
        };

        public static ContractorsRate ToEntity(this ContractorsRateDto dto) => new ContractorsRate
        {
            Id = dto.Id,
            JobTypeId = dto.JobTypeId,
            Pay = dto.Pay,
            ContractorId = dto.ContractorId,
        };


        public static ContractorsRateDto ToDto(this ContractorsRate rate) => new ContractorsRateDto
        {
            Id = rate.Id,
            JobTypeId = rate.JobTypeId,
            Pay = rate.Pay
        };

        public static PartnersRate ToEntity(this PartnersRateDto dto) => new PartnersRate
        {
            Id = dto.Id,
            JobTypeId = dto.JobTypeId,
            PartnerId = dto.PartnerId,
            Pay = dto.Pay
        };


        //public static PersonPayRatePerJobTypeDto ToDto(this PersonPayRatePerJobType rate) => new PersonPayRatePerJobTypeDto
        //{
        //    Id = rate.Id,
        //    JobTypeId = rate.JobTypeId,
        //    Pay = rate.Pay,
        //    ContractorId = rate.ContractorId
        //};

        //public static PersonPayRatePerJobType ToEntity(this PersonPayRatePerJobTypeDto dto) => new PersonPayRatePerJobType
        //{
        //    Id = dto.Id,
        //    JobTypeId = dto.JobTypeId,
        //    ContractorId = dto.ContractorId,
        //    Pay = dto.Pay
        //};


        //public static RolePayRatePerJobCategoryDto ToDto(this RolePayRatePerJobCategory rate) => new RolePayRatePerJobCategoryDto
        //{
        //    Id = rate.Id,
        //    JobCategoryId = rate.JobCategoryId,
        //    Pay = rate.Pay,
        //    RoleId = rate.RoleId
        //};

        //public static RolePayRatePerJobCategory ToEntity(this RolePayRatePerJobCategoryDto dto) => new RolePayRatePerJobCategory
        //{
        //    Id = dto.Id,
        //    JobCategoryId = dto.JobCategoryId,
        //    Pay = dto.Pay,
        //    RoleId = dto.RoleId
        //};

    }
}
