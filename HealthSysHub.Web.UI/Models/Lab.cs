namespace HealthSysHub.Web.UI.Models
{
    public class Lab
    {
        public Guid? LabId { get; set; }
        public string? LabName { get; set; }
        public string? LabCode { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
