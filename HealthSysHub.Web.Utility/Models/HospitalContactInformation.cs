using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Utility.Models
{
    public class HospitalContactInformation
    {
        public Guid? HospitalContactId { get; set; }
        public Guid? HospitalId { get; set; }
        public string? ContactType { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
