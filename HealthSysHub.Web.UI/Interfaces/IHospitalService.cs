using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IHospitalService
    {
        Task<Hospital> GetHospitalByIdAsync(Guid hospitalId);
        Task<List<Hospital>> GetHospitalsAsync();
        Task<Hospital> InsertOrUpdateHospitalAsync(Hospital hospital);
        Task<List<HospitalInformation>> GetHospitalInformationsAsync();
        Task<HospitalInformation> GetHospitalInformationByIdAsync(Guid hospitalId);

        Task<List<HospitalContactInformation>> InsertOrUpdateHospitalContactInformationAsync(HospitalContactInformation hospitalContactInformation);
        Task<HospitalContentInformation> InsertOrUpdateHospitalContentInformationAsync(HospitalContentInformation hospitalContentInformation);
        Task<HospitalDepartmentInformation> InsertOrUpdateHospitalDepartmentInformationAsync(HospitalDepartmentInformation hospitalDepartmentInformation);
        Task<HospitalSpecialtyInformation> InsertOrUpdateHospitalSpecialtyInformationAsync(HospitalSpecialtyInformation hospitalSpecialtyInformation);
    }
}
