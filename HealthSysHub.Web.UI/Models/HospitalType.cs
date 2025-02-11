namespace HealthSysHub.Web.UI.Models
{
    public class HospitalType
    {
        public Guid? HospitalTypeId { get; set; }
        public string? HospitalTypeName { get; set; }
        public string? Description { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
