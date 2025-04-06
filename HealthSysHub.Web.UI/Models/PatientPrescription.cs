namespace HealthSysHub.Web.UI.Models
{
    public class PatientPrescription
    {
        public Guid? PatientPrescriptionId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? ConsultationId { get; set; }
        public string? Treatment { get; set; }
        public string? Advice { get; set; }
        public string? Diagnosis { get; set; }
        public string? Notes { get; set; }
        public DateTimeOffset? FollowUpOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
