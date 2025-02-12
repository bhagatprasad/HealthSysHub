using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IStaffService
    {
        Task<List<HospitalStaff>> GetAllHospitalStaffAsync(Guid hosptialId);
        Task<HospitalStaff> GetHospitalStaffAsync(Guid hosptialId, Guid staffId);
        Task<HospitalStaff> InsertOrUpdateHospitalStaffAsync(HospitalStaff hospitalStaff);
    }
}
