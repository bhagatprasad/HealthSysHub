namespace HealthSysHub.Web.Utility.Models
{
    public class LabOrderRequestDetails
    {
        public LabOrderRequestDetails()
        {
            labOrderRequestItemDetails = new List<LabOrderRequestItemDetails>();
        }
        public Guid? LabOrderRequestId { get; set; }
        public string? HospitalName { get; set; }
        public string? DoctorName { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public List<LabOrderRequestItemDetails> labOrderRequestItemDetails { get; set; }
    }
}
