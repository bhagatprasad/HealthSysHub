using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("LabOrderRequestItem")]
    public class LabOrderRequestItem
    {
        [Key]
        public Guid LabOrderRequestItemId { get; set; } 
        public Guid? LabOrderRequestId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? TestId { get; set; }
        public decimal? ItemQty { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
