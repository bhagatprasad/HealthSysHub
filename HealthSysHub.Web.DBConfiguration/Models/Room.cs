using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("Room")]
    public class Room
    {
        [Key]
        public Guid RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public Guid? RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
        public int? FloorNumber { get; set; }
        public string? Wing { get; set; }
        public int? BedCount { get; set; }
        public bool? IsOccupied { get; set; }
        public DateTimeOffset? LastCleaned { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
