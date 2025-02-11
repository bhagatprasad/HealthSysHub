using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("HospitalStaff")]
    public class HospitalStaff
    {
        [Key]
        public Guid StaffId { get; set; }
        public Guid? HospitalId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Designation { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? SpecializationId { get; set; }
        public string? LicenseNumber { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
