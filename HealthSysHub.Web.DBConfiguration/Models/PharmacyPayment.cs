using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("PharmacyPayment")]
    public class PharmacyPayment
    {
        [Key]
        public Guid PaymentId { get; set; }
        public Guid? PharmacyOrderId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PharmacyId { get; set; }
        public string? PaymentNumber { get; set; } // NOT NULL UNIQUE
        public DateTimeOffset? PaymentDate { get; set; } = DateTimeOffset.Now;
        public string? PaymentMethod { get; set; } // NOT NULL, must be one of the specified values
        public decimal? PaymentAmount { get; set; } // NOT NULL
        public string? ReferenceNumber { get; set; } // Transaction ID, check number, etc.
        public string? Status { get; set; } // NOT NULL, must be one of the specified values
        public string? PaymentGateway { get; set; } // For online payments
        public string? GatewayResponse { get; set; } // Raw gateway response
        public string? Notes { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
