namespace HealthSysHub.Web.Utility.Models
{
    public class PharmacyOrderItemDetails
    {
        public Guid PharmacyOrderItemId { get; set; }
        public Guid? PharmacyId { get; set; }
        public Guid? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public long? ItemQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
