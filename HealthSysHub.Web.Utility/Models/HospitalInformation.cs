
namespace HealthSysHub.Web.Utility.Models
{
    public class HospitalInformation
    {
        public HospitalInformation()
        {
            hospitalContactInformation = new List<HospitalContactInformation>();
            hospitalContentInformation = new HospitalContentInformation();
            hospitalDepartmentInformation = new List<HospitalDepartmentInformation>();
            hospitalSpecialtyInformation = new List<HospitalSpecialtyInformation>();
        }
        public Guid? HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalCode { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }
        public Guid? HospitalTypeId { get; set; }
        public string? HospitalTypeName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public List<HospitalContactInformation> hospitalContactInformation { get; set; }
        public HospitalContentInformation hospitalContentInformation { get; set; }
        public List<HospitalDepartmentInformation> hospitalDepartmentInformation { get; set; }
        public List<HospitalSpecialtyInformation> hospitalSpecialtyInformation { get; set; }
    }
   
    
}
