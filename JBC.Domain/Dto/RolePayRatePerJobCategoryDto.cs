namespace JBC.Models.Dto
{
    public class RolePayRatePerJobCategoryDto
    {
        public int Id { get; set; }
        public int JobCategoryId { get; set; }
        public int RoleId { get; set; }
        public decimal Pay { get; set; } = 0;
    }
}
