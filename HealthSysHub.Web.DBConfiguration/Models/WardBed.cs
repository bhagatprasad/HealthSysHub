
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{

    [Table("WardBed")]
    public class WardBed
    {
        [Key]
        public Guid BedId { get; set; }
        public Guid? WardId { get; set; }
        public string? BedNumber { get; set; }
        public string? BedType { get; set; }
        public string? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }

}
