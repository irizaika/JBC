using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JBC.Models
{
    [Index(nameof(Name))]
    public class JobCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Full Day, Half Day, etc.
        public decimal? Hours { get; set; } // 8, 4, 12, or custom
        public bool IsCustom { get; set; } = false; // For custom jobs, true/false
    }
}
