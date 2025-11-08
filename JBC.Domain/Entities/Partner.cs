using JBC.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JBC.Domain.Entities
{
    [Index(nameof(CompanyName))]
    [Index(nameof(Status))]
    public class Partner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string ShortName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(30)]
        public string? Phone1 { get; set; }

        [MaxLength(30)]
        public string? Phone2 { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public ActivityStatus Status { get; set; } = ActivityStatus.NotSet;
    }
}
