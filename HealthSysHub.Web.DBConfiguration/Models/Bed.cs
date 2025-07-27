using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("Bed")]
    public class Bed
    {
        [Key]
        public Guid BedId { get; set; }
        public Guid? RoomId { get; set; }
        public string? BedNumber { get; set; }
        public string? BedType { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
