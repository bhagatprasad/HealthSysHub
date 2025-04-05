using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("PatientVital")]
    public class PatientVital
    {
        [Key]
        public Guid VitalId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? ConsultationId { get; set; }
        public string? BodyTemperature { get; set; }
        public string? HeartRate { get; set; }
        public string? BloodPressure { get; set; }
        public string? RespiratoryRate { get; set; }
        public string? OxygenSaturation { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? BMI { get; set; }
        public string? Notes { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
