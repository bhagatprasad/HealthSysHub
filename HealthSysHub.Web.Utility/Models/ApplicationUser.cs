namespace HealthSysHub.Web.Utility.Models
{
    public class ApplicationUser
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? RoleId { get; set; }
        public string? RoleName { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? StaffId { get; set; }
        public string? HospitalName { get; set; }
        public string? Designation { get; set; }
    }
}
