using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("Inpatient")]
    public class Inpatient
    {
        [Key]
        public Guid InpatientId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? HospitalId { get; set; }
        public DateTimeOffset? AdmissionDate { get; set; }
        public DateTimeOffset? DischargeDate { get; set; }
        public Guid? WardId { get; set; }
        public Guid? BedId { get; set; }
        public Guid? AdmittingDoctorId { get; set; }
        public string? CurrentStatus { get; set; }
        public string? ReasonForAdmission { get; set; }
        public int? ExpectedStayDuration { get; set; } 
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
