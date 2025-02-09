using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("HospitalType")]
    public class HospitalType
    {
        [Key]
        public Guid HospitalTypeId { get; set; } 
        public string? HospitalTypeName { get; set; } 
        public string? Description { get; set; } 
        public Guid? CreatedBy { get; set; }  
        public DateTimeOffset? CreatedOn { get; set; } 
        public Guid? ModifiedBy { get; set; } 
        public DateTimeOffset? ModifiedOn { get; set; }  
        public bool IsActive { get; set; }
    }
}
