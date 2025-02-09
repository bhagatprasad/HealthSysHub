using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Utility.Models
{
    public class HospitalSpecialtyInformation
    {
        public Guid? HospitalSpecialtyId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? SpecializationId { get; set; }
        public string? SpecializationName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
