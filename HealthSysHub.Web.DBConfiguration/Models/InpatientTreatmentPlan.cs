using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("InpatientTreatmentPlan")]
    public class InpatientTreatmentPlan
    {
        [Key]
        public Guid TreatmentPlanId { get; set; }
        public Guid? InpatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public string? PlanDetails { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? ExpectedEndDate { get; set; }
        public string? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
