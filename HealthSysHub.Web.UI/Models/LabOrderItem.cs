namespace HealthSysHub.Web.UI.Models
{
    public class LabOrderItem
    {
        public Guid? LabOrderItemId { get; set; }
        public Guid? LabOrderId { get; set; }
        public Guid? TestId { get; set; }
        public long? ItemQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
