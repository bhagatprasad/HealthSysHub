using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("RoomType")]
    public class RoomType
    {
        [Key]
        public Guid RoomTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? Description { get; set; }
        public decimal? BaseRate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
