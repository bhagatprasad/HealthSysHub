using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("HospitalSpecialty")]
    public class HospitalSpecialty
    {
        [Key]
        public Guid HospitalSpecialtyId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? SpecializationId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
