using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JBC.Domain.Entities
{
    [Index(nameof(PartnerId))]
    [Index(nameof(JobTypeId))]
    public class PartnersRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PartnerId { get; set; }

        [Required]
        public int JobTypeId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Pay { get; set; } = 0;
    }
}
