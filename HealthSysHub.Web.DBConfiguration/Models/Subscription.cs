using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("Subscription")]
    public class Subscription
    {
        [Key]
        public Guid SubscriptionId { get; set; }
        public Guid? PharmacyId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? LabId { get; set; }
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
        public string? SubscriptionType { get; set; }
        public string? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }

}
