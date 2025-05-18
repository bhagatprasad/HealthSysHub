namespace HealthSysHub.Web.UI.Models
{
    public class PharmacyStaff
    {
        public Guid? StaffId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Designation { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PharmacyId { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
