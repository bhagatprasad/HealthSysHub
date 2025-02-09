using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("HospitalDepartment")]
    public class HospitalDepartment
    {
        [Key]
        public Guid HospitalDepartmentId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string HeadOfDepartment { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
