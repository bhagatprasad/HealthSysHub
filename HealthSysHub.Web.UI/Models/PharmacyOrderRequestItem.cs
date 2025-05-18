namespace HealthSysHub.Web.UI.Models
{
    public class PharmacyOrderRequestItem
    {
        public Guid? PharmacyOrderRequestItemId { get; set; }
        public Guid? PharmacyOrderRequestId { get; set; }
        public Guid? PharmacyId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? MedicineId { get; set; }
        public decimal? ItemQty { get; set; }
        public string? Usage { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
