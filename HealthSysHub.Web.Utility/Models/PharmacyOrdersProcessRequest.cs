namespace HealthSysHub.Web.Utility.Models
{
    public class PharmacyOrdersProcessRequest
    {
        public Guid? PharmacyOrderId { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
