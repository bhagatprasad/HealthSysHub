using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("PharmacyInvoiceItem")]
    public class PharmacyInvoiceItem
    {
        [Key]
        public Guid InvoiceItemId { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid? PharmacyOrderId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PharmacyId { get; set; }
        public Guid? PharmacyOrderRequestId { get; set; }
        public Guid? MedicineId { get; set; }
        public long? ItemQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
