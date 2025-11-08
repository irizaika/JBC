using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JBC.Domain.Entities
{
    [Index(nameof(ContractorId))]
    [Index(nameof(JobTypeId))]
    public class PersonPayRatePerJobType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(JobType))]
        public int JobTypeId { get; set; }
        public JobType JobType { get; set; }// navigation property

        [Required]
        [ForeignKey(nameof(Contractor))]
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }// navigation property

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Pay { get; set; } = 0;
    }
}
