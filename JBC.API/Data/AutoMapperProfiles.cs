//using AutoMapper;
//using JBC.Domain.Entities;
//using JBC.Domain.Dto;

//namespace JBC.Data
//{
//    public class VanProfile : Profile
//    {
//        public VanProfile()
//        {
//            CreateMap<Van, VanDto>().ReverseMap();
//        }
//    }

//    public class JobProfile : Profile
//    {
//        public JobProfile()
//        {
//            //  CreateMap<Job, JobDto>().ReverseMap();

//            //CreateMap<Job, JobDto>()
//            //  .ForMember(dest => dest.JobTypeName,
//            //      opt => opt.MapFrom(src => src.JobType != null ? src.JobType.Name : null))
//            //  .ForMember(dest => dest.PartnerName,
//            //      opt => opt.MapFrom(src => src.Partner != null ? src.Partner.CompanyName : null))
//            //  .ForMember(dest => dest.Contractors,
//            //      opt => opt.MapFrom(src => src.ContractorList.Select(c => c.Name))) 
//            //  .ForMember(dest => dest.Vans,
//            //      opt => opt.MapFrom(src => src.VanList.Select(v => v.RegistrationNumber)));

//            CreateMap<Job, JobDto>()
//                .ForMember(dest => dest.PartnerName,
//                    opt => opt.MapFrom(src => src.Partner != null ? src.Partner.CompanyName : null))
//                .ForMember(dest => dest.JobTypeName,
//                    opt => opt.MapFrom(src => src.JobType != null ? src.JobType.Name : null))
//                .ForMember(dest => dest.Contractors,
//                    opt => opt.MapFrom(src => src.JobContractors != null
//                        ? src.JobContractors.Select(jc =>  new JobContractorDto {ContractorId = jc.ContractorId, Pay=jc.Pay } /*, ContractorName = jc.Contractor.ShortName*/ )
//                        : Enumerable.Empty<JobContractorDto>()))
//                .ForMember(dest => dest.Vans,
//                    opt => opt.MapFrom(src => src.JobVans != null
//                        ? src.JobVans.Select(jv => jv.VanId/*, VanName = jv.Van.VanName*/)
//                        : Enumerable.Empty<int>()))
//                .ReverseMap()
//                .ForMember(dest => dest.JobContractors, opt => opt.MapFrom(src =>
//                    src.Contractors != null
//                        ? src.Contractors.Select(c => new JobContractor { ContractorId = c.ContractorId, Pay = c.Pay, JobId = src.Id })
//                        : new List<JobContractor>()))
//                .ForMember(dest => dest.JobVans, opt => opt.MapFrom(src =>
//                    src.Vans != null
//                        ? src.Vans.Select(v => new JobVan { VanId = v, JobId = src.Id })
//                        : new List<JobVan>()))
//                .ForMember(dest => dest.Partner, opt => opt.Ignore())
//                .ForMember(dest => dest.JobType, opt => opt.Ignore());
//        }
    
//    }

//    public class ContractorProfile : Profile
//    {
//        public ContractorProfile()
//        {
//            CreateMap<Contractor, ContractorDto>().ReverseMap();
//        }
//    }

//    public class PartnerProfile : Profile
//    {
//        public PartnerProfile()
//        {
//            CreateMap<Partner, PartnerDto>().ReverseMap();
//        }
//    }

//    public class RoleProfile : Profile
//    {
//        public RoleProfile()
//        {
//            CreateMap<Role, RoleDto>().ReverseMap();
//        }
//    }

//    public class JobTypeProfile : Profile
//    {
//        public JobTypeProfile()
//        {
//            CreateMap<JobType, JobTypeDto>().ReverseMap();
//        }
//    }

//    public class PartnersRateProfile : Profile
//    {
//        public PartnersRateProfile()
//        {
//            CreateMap<PartnersRate, PartnersRateDto>().ReverseMap();
//        }
//    }

//    public class ContractorsRateProfile : Profile
//    {
//        public ContractorsRateProfile()
//        {
//            CreateMap<ContractorsRate, ContractorsRateDto>().ReverseMap();
//        }
//    }

//}