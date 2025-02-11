namespace HealthSysHub.Web.UI.Models
{
    public class PatientType
    {
        public Guid? PatientTypeId { get; set; }

        public string? PatientTypeName { get; set; }

        public string? Description { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
