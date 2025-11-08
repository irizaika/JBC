using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JBC.Models
{
    [Index(nameof(Date))]
    [Index(nameof(PartnerId))]
    [Index(nameof(JobTypeId))]
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.Date);
      
        [ForeignKey(nameof(Partner))] // Nullable FK → allows own jobs with no partner
        public int? PartnerId { get; set; }
        public Partner? Partner { get; set; } // navigation property 

        [MaxLength(150)]
        public string? CustomerName { get; set; }

        [MaxLength(500)]
        public string? Details { get; set; }


        // 1 = full day, 0.5 = half day, etc.
        [Column(TypeName = "decimal(5,2)")]
        public decimal Count { get; set; } = 1.0m;

        [Column(TypeName = "decimal(10,2)")]
        public decimal PayReceived { get; set; } = 0m;

        [ForeignKey(nameof(JobType))]
        public int? JobTypeId { get; set; }  // Nullable FK → allows own jobs with no jobtype
        public JobType? JobType { get; set; }  // navigation property 
        public ICollection<JobContractor> JobContractors { get; set; } = new List<JobContractor>();// navigation property 
        public ICollection<JobVan> JobVans { get; set; } = new List<JobVan>();// navigation property 
    }

    public class JobContractor
    {
        [ForeignKey(nameof(Job))] 
        public int JobId { get; set; }
        public Job Job { get; set; } // navigation property
                                     // 
        [ForeignKey(nameof(Contractor))] 
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; } // navigation property 
        public double Pay { get; set; }
    }

    public class JobVan
    {
        [ForeignKey(nameof(Job))] 
        public int JobId { get; set; }
        public Job Job { get; set; } // navigation property 

        [ForeignKey(nameof(Van))] 
        public int VanId { get; set; }
        public Van Van { get; set; }// navigation property 
    }
}
