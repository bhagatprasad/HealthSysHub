namespace HealthSysHub.Web.Utility.Models
{
    public class LabOrderDetails
    {
        public LabOrderDetails()
        {
            labOrderItemDetails = new List<LabOrderItemDetails>();
        }
        public Guid LabOrderId { get; set; }
        public Guid? LabOrderRequestId { get; set; }
        public long? ItemQty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? FinalAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public List<LabOrderItemDetails> labOrderItemDetails { get; set; }
    }
}
