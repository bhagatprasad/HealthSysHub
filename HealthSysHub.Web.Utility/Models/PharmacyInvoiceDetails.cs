namespace HealthSysHub.Web.Utility.Models
{
    public class PharmacyInvoiceDetails
    {
        public PharmacyInvoiceDetails()
        {
            pharmacyInvoiceItemDetails = new List<PharmacyInvoiceItemDetails>();
        }
        public Guid? InvoiceId { get; set; }
        public Guid? PharmacyOrderId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PharmacyId { get; set; }
        public Guid? PharmacyOrderRequestId { get; set; }
        public string? InvoiceReference { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TaxAmount { get; set; } = 0;
        public decimal? DiscountAmount { get; set; } = 0;
        public decimal? TotalAmount { get; set; }
        public decimal? AmountPaid { get; set; } = 0;
        public decimal? BalanceDue { get; set; }
        public string? Status { get; set; } // e.g., 'Draft', 'Sent', 'Paid', 'Partial', 'Overdue', 'Cancelled'
        public string? Notes { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; } = true;

        public List<PharmacyInvoiceItemDetails> pharmacyInvoiceItemDetails { get; set; }
    }
}
