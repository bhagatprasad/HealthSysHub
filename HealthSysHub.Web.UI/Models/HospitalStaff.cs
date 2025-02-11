namespace HealthSysHub.Web.UI.Models
{
    
    public class HospitalStaff
    {
     
        public Guid? StaffId { get; set; }
        public Guid? HospitalId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Designation { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? SpecializationId { get; set; }
        public string? LicenseNumber { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
