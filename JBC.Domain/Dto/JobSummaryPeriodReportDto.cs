namespace JBC.Domain.Dto
{
    public class JobSummaryPeriodReportDto
    {
        public string Period { get; set; }
        public string Key { get; set; }
        public DateOnly Date { get; set; }
        public int TotalJobs { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal TotalPaidToContractors { get; set; }
        public decimal Profit { get; set; }
    }
}
