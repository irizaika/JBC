using JBC.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JBC.Domain.Entities
{
    [Index(nameof(VanName))]
    [Index(nameof(Status))]
    public class Van
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string VanName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Details { get; set; }

        [MaxLength(20)]
        public string? Plate { get; set; }

        public ActivityStatus Status { get; set; } = ActivityStatus.NotSet;
    }
}
