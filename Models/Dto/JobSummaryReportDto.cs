namespace JBC.Models.Dto
{
    public class JobSummaryReportDto
    {
        public DateOnly Date { get; set; }
        public int TotalJobs { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal TotalPaidToContractors { get; set; }
        public decimal Profit => TotalReceived - TotalPaidToContractors;
    }
}
