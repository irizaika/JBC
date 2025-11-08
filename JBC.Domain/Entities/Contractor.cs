using JBC.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JBC.Domain.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(Status))]
    public class Contractor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string ShortName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(30)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? BankAccount { get; set; }

        public ActivityStatus Status { get; set; } = ActivityStatus.NotSet;

        [Column(TypeName = "decimal(10,2)")]
        public decimal DayRate { get; set; } = 0;

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        // Navigation property
        public Role Role { get; set; } = null!;
    }
}
