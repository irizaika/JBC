using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JBC.Models
{
    [Index(nameof(ContractorId))]
    [Index(nameof(JobTypeId))]
    public class ContractorsRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Contractor))]
        public int ContractorId { get; set; }
        public Contractor? Contractor { get; set; }

        [ForeignKey(nameof(JobType))]
        public int JobTypeId { get; set; }
        public JobType? JobType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Pay { get; set; } = 0;
    }
}
