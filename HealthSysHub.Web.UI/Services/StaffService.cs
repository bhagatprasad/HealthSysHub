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
