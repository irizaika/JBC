namespace JBC.Models.Dto
{
    public class ContractorsRateDto
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public int JobTypeId { get; set; }
        public decimal Pay { get; set; } = 0;
    }
}
