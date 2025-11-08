using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JBC.Models
{
    [Index(nameof(JobCategoryId))]
    [Index(nameof(RoleId))]
    public class RolePayRatePerJobCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(JobCategory))]
        public int JobCategoryId { get; set; }
        public JobCategory JobCategory { get; set; }// navigation property

        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role Role { get; set; }// navigation property

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Pay { get; set; } = 0;
    }
}
