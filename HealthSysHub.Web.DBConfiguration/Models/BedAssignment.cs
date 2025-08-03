using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("BedAssignment")]
    public class BedAssignment
    {
        [Key]
        public Guid AssignmentId { get; set; } 
        public Guid? AdmissionId { get; set; }
        public Guid? WardId { get; set; }
        public Guid? BedId { get; set; }
        public DateTimeOffset? AssignedDate { get; set; }
        public DateTimeOffset? DischargedDate { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }

}
