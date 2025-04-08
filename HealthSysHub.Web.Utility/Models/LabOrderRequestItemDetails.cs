namespace HealthSysHub.Web.Utility.Models
{
    public class LabOrderRequestItemDetails
    {
        public Guid? LabOrderRequestItemId { get; set; }
        public Guid? TestId { get; set; }
        public string? TestName { get; set; }
        public decimal? ItemQty { get; set; }
    }
}
