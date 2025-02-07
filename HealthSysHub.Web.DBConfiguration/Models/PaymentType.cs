using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("PaymentType")]
    public class PaymentType
    {
        [Key]
        public Guid PaymentTypeId { get; set; }

        public string? PaymentTypeName { get; set; } 
        public string? Description { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
