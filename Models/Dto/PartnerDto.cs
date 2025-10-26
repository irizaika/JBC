namespace JBC.Models.Dto
{
    public class PartnerDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? ShortName { get; set; }
        public string? Address { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Email { get; set; }
        public int Status { get; set; } = 0;
    }
}
