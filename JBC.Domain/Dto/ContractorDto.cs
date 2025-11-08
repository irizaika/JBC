using JBC.Data;

namespace JBC.Models.Dto
{
    public class ContractorDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? BankAccount { get; set; }

        public ActivityStatus Status { get; set; } = ActivityStatus.NotSet;

        public decimal DayRate { get; set; } = 0;

        public int RoleId { get; set; }

        public Dictionary<int, decimal> RoleRates { get; set; } = new();
        public Dictionary<int, decimal> ContractorRates { get; set; } = new();

    }
}
