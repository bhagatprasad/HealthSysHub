using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class StaffService : IStaffService
    {
        public Task<List<HospitalStaff>> GetAllHospitalStaffAsync(Guid hosptialId)
        {
            throw new NotImplementedException();
        }

        public Task<HospitalStaff> GetHospitalStaffAsync(Guid hosptialId, Guid staffId)
        {
            throw new NotImplementedException();
        }

        public Task<HospitalStaff> InsertOrUpdateHospitalStaffAsync(HospitalStaff hospitalStaff)
        {
            throw new NotImplementedException();
        }
    }
}
