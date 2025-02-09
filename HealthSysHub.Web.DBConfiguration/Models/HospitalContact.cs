using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("HospitalContact")]
    public class HospitalContact
    {
        [Key]
        public Guid HospitalContactId { get; set; }
        public Guid? HospitalId { get; set; }
        public string? ContactType { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
