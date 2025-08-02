using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("InpatientMedication")]
    public class InpatientMedication
    {
        [Key]
        public Guid MedicationId { get; set; }
        public Guid? InpatientId { get; set; }
        public Guid? MedicineId { get; set; }
        public Guid? DoctorId { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public DateTimeOffset? StartDate { get; set; } 
        public DateTimeOffset? EndDate { get; set; }
        public string? Status { get; set; }
        public string Notes { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
