namespace HealthSysHub.Web.Utility.Models
{
    public class PatientPrescriptionDetails
    {
        public PatientPrescriptionDetails()
        {
            labOrderRequestDetails = new LabOrderRequestDetails();
            labOrderDetails = new LabOrderDetails();
            pharmacyOrderDetails = new PharmacyOrderDetails();
            pharmacyOrderRequestDetails = new PharmacyOrderRequestDetails();
        }
        public Guid? PatientPrescriptionId { get; set; }
        public string? Treatment { get; set; }
        public string? Advice { get; set; }
        public string? Diagnosis { get; set; }
        public string? Notes { get; set; }
        public DateTimeOffset? FollowUpOn { get; set; }
        public LabOrderDetails labOrderDetails { get; set; }
        public LabOrderRequestDetails labOrderRequestDetails { get; set; }
        public PharmacyOrderDetails pharmacyOrderDetails { get; set; }
        public PharmacyOrderRequestDetails pharmacyOrderRequestDetails { get; set; }
    }
}
