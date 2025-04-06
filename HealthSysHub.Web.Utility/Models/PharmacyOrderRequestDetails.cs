namespace HealthSysHub.Web.Utility.Models
{
    public class PharmacyOrderRequestDetails
    {
        public PharmacyOrderRequestDetails()
        {
            pharmacyOrderRequestItemDetails = new List<PharmacyOrderRequestItemDetails>();
        }
        public Guid PharmacyOrderRequestId { get; set; }
        public Guid? PatientPrescriptionId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PatientId { get; set; }
        public string? HospitalName { get; set; }
        public string? DoctorName { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public List<PharmacyOrderRequestItemDetails> pharmacyOrderRequestItemDetails { get; set; }
    }
}
