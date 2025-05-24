namespace HealthSysHub.Web.Utility.Models
{
    public class ResetPassword
    {
        public Guid? Id { get; set; }
        public Guid? StaffId { get; set; }
        public string Password { get; set; }
        public Guid? ModifiedBy { get; set; } 
        public DateTimeOffset? ModifiedOn { get; set; } 
    }
}
