namespace JBC.Models.Dto
{
    public class VanReportDto
    {
        public int VanId { get; set; }
        public string VanName { get; set; } = string.Empty;
        public int TotalJobs { get; set; }
        public DateTime? FirstJobDate { get; set; }
        public DateTime? LastJobDate { get; set; }
    }
}
