namespace HealthSysHub.Web.Utility.Models
{
    public class PharmacyPaymentDetail : PharmacyOrderDetails
    {
        public Guid? PaymentId { get; set; }
        public Guid? HospitalId { get; set; }
        public string? PaymentNumber { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal? PaymentAmount { get; set; } 
        public string? ReferenceNumber { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentGateway { get; set; }
        public string? GatewayResponse { get; set; }
        public string? PaymentNotes { get; set; }
    }
}
