﻿namespace HealthSysHub.Web.UI.Models
{
    public class PharmacyMedicine
    {
        public Guid? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public string? GenericName { get; set; }
        public string? DosageForm { get; set; }
        public string? Strength { get; set; }
        public string? Manufacturer { get; set; }
        public string? BatchNumber { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
        public decimal? PricePerUnit { get; set; }
        public int StockQuantity { get; set; }
        public Guid? PharmacyId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
