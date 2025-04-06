
namespace HealthSysHub.Web.Utility.Models
{
    public class LabOrderItemDetails
    {
        public Guid LabOrderItemId { get; set; }
        public Guid? TestId { get; set; }
        public string? TestName { get; set; }
        public long? ItemQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
