using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("InpatientAdmission")]
    public class InpatientAdmission
    {
        [Key]
        public Guid AdmissionId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? AdmittingDoctorId { get; set; }
        public DateTimeOffset? AdmissionDate { get; set; }
        public string? AdmissionType { get; set; }
        public string? ReasonForAdmission { get; set; }
        public int? ExpectedStayDuration { get; set; }
        public DateTimeOffset? DischargeDate { get; set; }
        public string? DischargeStatus { get; set; }
        public string? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; } 
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
