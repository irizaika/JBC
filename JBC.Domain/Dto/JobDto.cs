namespace JBC.Domain.Dto
{
    //    public class JobDto
    //    {
    //        public int Id { get; set; }

    //        public DateTime Date { get; set; }

    //        // Nullable → allows own jobs with no partner
    //        public int? PartnerId { get; set; } = null;

    //        public string? CustomerName { get; set; } = string.Empty;

    //        public string? Details { get; set; } = string.Empty;

    //        // JSON or comma-separated contractor IDs
    //        public string? ContractorList { get; set; } = string.Empty;

    //        // 1 = full day, 0.5 = half day, etc.
    //        public decimal Count { get; set; } = 1.0m;

    //        public decimal PayReceived { get; set; } = 0.0m;

    //        // Nullable → job may have no specific type
    //        public int? JobTypeId { get; set; } = null;

    //        // JSON or comma-separated van IDs
    //        public string? VanList { get; set; } = string.Empty;
    //    }

    public class JobDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }

        // Job Type
        public int? JobTypeId { get; set; } = null;
        public string? JobTypeName { get; set; } = string.Empty;

        // Partner
        public int? PartnerId { get; set; } = null;
        public string? PartnerName { get; set; } = string.Empty;

        // Other fields
        public string? CustomerName { get; set; }
        public string? Details { get; set; }
        public decimal Count { get; set; } = 1.0m;
        public decimal PayReceived { get; set; } = 0.0m;

        // Contractors (list of linked people, optional)
        public List<JobContractorDto>? Contractors { get; set; }

        // Vans (list of van registration numbers or names)
        public List<int>? Vans { get; set; }
    }

    public class JobContractorDto
    {
        public int ContractorId { get; set; }
        public double Pay { get; set; }

        //    public string ContractorName { get; set; }
    }

    public class JobVanDto
    {
        public int VanId { get; set; }
  //      public string VanName { get; set; }
    }
}
