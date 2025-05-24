namespace HealthSysHub.Web.Utility.Models
{
    public class ForgotPasswordResponse
    {
        public Guid? Id { get; set; }
        public Guid? StaffId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
