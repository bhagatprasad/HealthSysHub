
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("PharmacyOrderType")]
    public class PharmacyOrderType
    {
        [Key]
        public Guid PharmacyOrderTypeId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
