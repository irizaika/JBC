namespace JBC.Models.Dto
{
    public class JobTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? JobCategoryId { get; set; } = null;
        public string? Description { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfVans { get; set; }
        public int? PartnerId { get; set; }
        public decimal PayRate { get; set; } = 0;
    }
}
