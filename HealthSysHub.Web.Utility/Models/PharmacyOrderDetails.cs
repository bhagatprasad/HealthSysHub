namespace HealthSysHub.Web.Utility.Models
{
    public class PharmacyOrderDetails
    {
        public PharmacyOrderDetails()
        {
            pharmacyOrderItemDetails = new List<PharmacyOrderItemDetails>();
        }
        public Guid PharmacyOrderId { get; set; }
        public Guid? PharmacyOrderRequestId { get; set; }
        public long? ItemQty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? FinalAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public List<PharmacyOrderItemDetails> pharmacyOrderItemDetails { get; set; }
    }
}
