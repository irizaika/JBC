namespace JBC.Models.Dto
{
    public class VanDto
    {
        public int Id { get; set; }
        public string VanName { get; set; } = string.Empty;
        public string? Details { get; set; }
        public string? Plate { get; set; }
        public int Status { get; set; } = 0;
    }
}
