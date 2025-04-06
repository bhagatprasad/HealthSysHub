namespace HealthSysHub.Web.Utility.Models
{
    public class ConsultationDetails
    {
        public ConsultationDetails()
        {
            patientDetails = new PatientDetails();
        }
        public Guid ConsultationId { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? DoctorId { get; set; }
        public string? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public PatientDetails patientDetails { get; set; }
    }
}
