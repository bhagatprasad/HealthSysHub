using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IHospitalTypeService
    {
        Task<List<HospitalType>> GetHospitalTypesAsync();
        Task<HospitalType> GetHospitalTypeAsync(Guid hospitalTypeId);
        Task<HospitalType> InsertOrUpdateHospitalTypeAsync(HospitalType hospitalType);
    }
}
