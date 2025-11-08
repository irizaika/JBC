namespace JBC.Domain.Dto
{
    public class ContractorReportDto
    {
        public int ContractorId { get; set; }
        public string ContractorName { get; set; } = string.Empty;
        public int TotalJobs { get; set; }
        public decimal TotalPay { get; set; }
        public decimal AveragePayPerJob { get; set; }
        public List<JobPerPartner> partnerJobList {get;set;}
    }

    public class JobPerPartner
    {
        public string Name { get; set; } = string.Empty;
        public int count { get; set; }
    }

}
