namespace JBC.Domain.Dto
{

    public class JobCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Full Day, Half Day, etc.
        public decimal Hours { get; set; } // 8, 4, 12, or custom
        public bool IsCustom { get; set; } // For custom jobs, true/false
    }
}
