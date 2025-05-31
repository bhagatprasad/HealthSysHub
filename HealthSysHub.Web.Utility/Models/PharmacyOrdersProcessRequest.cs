namespace HealthSysHub.Web.Utility.Models
{
    public class PharmacyOrdersProcessRequest
    {
        public Guid? PharmacyOrderId { get; set; }
        public Guid? PharmacyId { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public Guid? CreatedBy { get; set; }           // string | null
        public DateTimeOffset? CreatedOn { get; set; }        // Date | string | null
        public Guid? ModifiedBy { get; set; }          // string | null
        public DateTimeOffset? ModifiedOn { get; set; }       // Date | string | null
        public bool IsActive { get; set; }
    }
}
