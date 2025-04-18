namespace HealthSysHub.Web.Utility.Models
{
    public class LabOrderRequestDetails
    {
        public LabOrderRequestDetails()
        {
            labOrderRequestItemDetails = new List<LabOrderRequestItemDetails>();
        }
        public Guid? LabOrderRequestId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PatientPrescriptionId { get; set; }
        public Guid? PatientId { get; set; }
        public string? HospitalName { get; set; }
        public string? DoctorName { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public List<LabOrderRequestItemDetails> labOrderRequestItemDetails { get; set; }
    }
}
