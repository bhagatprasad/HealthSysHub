using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? HospitalId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? StaffId { get; set; }
        public DateTimeOffset? LastPasswordChangedOn { get; set; }
        public bool? IsBlocked { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
