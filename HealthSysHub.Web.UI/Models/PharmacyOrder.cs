﻿namespace HealthSysHub.Web.UI.Models
{
    public class PharmacyOrder
    {
        public Guid? PharmacyOrderId { get; set; }
        public Guid? PharmacyOrderRequestId { get; set; }
        public long? ItemQty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? FinalAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
