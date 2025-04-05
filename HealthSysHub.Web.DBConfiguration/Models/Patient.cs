using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("Patient")]
    public class Patient
    {
        [Key]
        public Guid PatientId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? ConsultationId { get; set; }
        public Guid? PatientTypeId { get; set; }
        public string? HealthIssue { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? AttenderPhone { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
