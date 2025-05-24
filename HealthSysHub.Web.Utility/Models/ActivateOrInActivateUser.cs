namespace HealthSysHub.Web.Utility.Models
{
    public class ActivateOrInActivateUser
    {
        public Guid? Id { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? LabId { get; set; }
        public Guid? PharmacyId { get; set; }
        public bool IsActive { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
