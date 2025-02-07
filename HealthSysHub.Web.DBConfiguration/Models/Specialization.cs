using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("Specialization")]
    public class Specialization
    {
        [Key]
        public Guid SpecializationId { get; set; }

        public string? SpecializationName { get; set; }

        public string? SpecializationDescription { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
