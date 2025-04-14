using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class StaffService : IStaffService
    {
        private readonly IRepositoryFactory _repository;

        public StaffService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<List<HospitalStaff>> GetAllHospitalStaffAsync(Guid hosptialId)
        {
            var uri = Path.Combine("Staff/GetAllHospitalStaffAsync", hosptialId.ToString());
            return await _repository.SendAsync<List<HospitalStaff>>(HttpMethod.Get, uri);
        }

        public async Task<HospitalStaff> GetHospitalStaffAsync(Guid hosptialId, Guid staffId)
        {
            var uri = Path.Combine("Staff/GetHospitalStaffAsync", hosptialId.ToString() + "/" + staffId.ToString());
            return await _repository.SendAsync<HospitalStaff>(HttpMethod.Get, uri);
        }

        public async Task<HospitalStaff> InsertOrUpdateHospitalStaffAsync(HospitalStaff hospitalStaff)
        {
            return await _repository.SendAsync<HospitalStaff, HospitalStaff>(HttpMethod.Post, "Staff/InsertOrUpdateHospitalStaffAsync", hospitalStaff);
        }
    }
}
