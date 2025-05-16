using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("PharmacyOrderItem")]
    public class PharmacyOrderItem
    {
        [Key]
        public Guid PharmacyOrderItemId { get; set; }
        public Guid? PharmacyOrderId { get; set; }
        public Guid? PharmacyId { get; set; }
        public Guid? MedicineId { get; set; }
        public long? ItemQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
