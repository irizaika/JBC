namespace JBC.Models.Dto
{
    public class PartnersRateDto
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public int JobTypeId { get; set; }
        public decimal Pay { get; set; } = 0;
    }
}
