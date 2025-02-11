namespace HealthSysHub.Web.UI.Models
{
    public class PaymentType
    {
        public Guid? PaymentTypeId { get; set; }

        public string? PaymentTypeName { get; set; }

        public string? Description { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
