namespace JBC.Models.Dto
{
    public class PartnerJobSummaryDto
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public double TotalJobs { get; set; }
        public decimal TotalPayReceived { get; set; }
        public decimal TotalContractorCost { get; set; }
        public decimal Profit => TotalPayReceived - TotalContractorCost;
        public decimal AvgPayPerJob => TotalJobs > 0 ? (decimal)TotalPayReceived / (decimal)TotalJobs : 0;
    }
}
