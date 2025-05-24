namespace HealthSysHub.Web.Utility.Models
{
    public class ChangePassword
    {
        public Guid? Id { get; set; }                   // Guid? in C#
        public Guid? HospitalId { get; set; }           // Guid? in C#
        public Guid? StaffId { get; set; }              // Guid? in C#
        public Guid? LabId { get; set; }                // Guid? in C# (new property)
        public Guid? PharmacyId { get; set; }           // Guid? in C# (new property)
        public string Password { get; set; }
        public Guid? CreatedBy { get; set; }           // string | null
        public DateTimeOffset? CreatedOn { get; set; }        // Date | string | null
        public Guid? ModifiedBy { get; set; }          // string | null
        public DateTimeOffset? ModifiedOn { get; set; }       // Date | string | null
    }

}
