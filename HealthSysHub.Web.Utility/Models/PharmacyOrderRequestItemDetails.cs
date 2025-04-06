namespace HealthSysHub.Web.Utility.Models
{
    public class PharmacyOrderRequestItemDetails
    {
        public Guid PharmacyOrderRequestItemId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public decimal? ItemQty { get; set; }
        public string? Usage { get; set; }
      
    }
}
