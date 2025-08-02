using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("Ward")]
    public class Ward
    {
        [Key]
        public Guid? WardId { get; set; }
        public Guid? HospitalId { get; set; }
        public string? WardName { get; set; }
        public string? WardType { get; set; } 
        public int? Capacity { get; set; }
        public int? CurrentOccupancy { get; set; } 
        public string? Description { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
