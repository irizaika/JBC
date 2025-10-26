using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JBC.Models
{
    [Index(nameof(Name))]
    public class JobType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(JobCategory))] // Nullable FK → allows own jobs with no category
        public int? JobCategoryId { get; set; }
        public JobCategory? JobCategory { get; set; } // navigation property 

        [MaxLength(200)]
        public string? Description { get; set; }

        public int NumberOfPeople { get; set; }

        public int NumberOfVans { get; set; }

        [ForeignKey(nameof(Partner))]
        public int? PartnerId { get; set; }

        public Partner? Partner { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PayRate { get; set; } = 0;
    }
}
