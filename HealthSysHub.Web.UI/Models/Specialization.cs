namespace HealthSysHub.Web.UI.Models
{
    public class Specialization
    {
        public Guid? SpecializationId { get; set; }

        public string? SpecializationName { get; set; }

        public string? SpecializationDescription { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
