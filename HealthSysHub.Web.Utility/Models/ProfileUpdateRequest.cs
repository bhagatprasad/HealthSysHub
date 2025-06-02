namespace HealthSysHub.Web.Utility.Models
{
    public class ProfileUpdateRequest
    {
        public Guid? Id { get; set; }
        public string? EntityType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PharmacyId { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? LabId { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }

    }
}
