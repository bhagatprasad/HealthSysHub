using HealthSysHub.Web.DBConfiguration.Models;


namespace HealthSysHub.Web.Managers
{
    public interface IStaffManager
    {
        Task<List<HospitalStaff>> GetAllHospitalStaffAsync(Guid hosptialId);
        Task<HospitalStaff> GetHospitalStaffAsync(Guid hosptialId,Guid staffId);
        Task<HospitalStaff> InsertOrUpdateHospitalStaffAsync(HospitalStaff hospitalStaff);
    }
}
