namespace HealthSysHub.Web.UI.Models
{
    public class Department
    {
        public Guid? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentDescription { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
