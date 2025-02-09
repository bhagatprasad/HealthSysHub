using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("Hospital")]
    public class Hospital
    {
        [Key]
        public Guid HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalCode { get; set; } 
        public string? RegistrationNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; } 
        public string? Website { get; set; } 
        public string? LogoUrl { get; set; }
        public Guid? HospitalTypeId { get; set; } 
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
