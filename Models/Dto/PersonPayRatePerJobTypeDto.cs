
namespace JBC.Models
{
    public class PersonPayRatePerJobTypeDto
    {
        public int Id { get; set; }
        public int JobTypeId { get; set; }
        public int ContractorId { get; set; }
        public decimal Pay { get; set; } = 0;
    }
}
