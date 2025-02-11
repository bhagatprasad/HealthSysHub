using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class HospitalTypeService : IHospitalTypeService
    {
        private readonly IRepositoryFactory _repository;

        public HospitalTypeService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public async Task<HospitalType> GetHospitalTypeAsync(Guid hospitalTypeId)
        {
            var uri = Path.Combine("HospitalType/GetHospitalTypeByIdAsync", hospitalTypeId.ToString());
            return await _repository.SendAsync<HospitalType>(HttpMethod.Get, uri);
        }

        public async Task<List<HospitalType>> GetHospitalTypesAsync()
        {
            return await _repository.SendAsync<List<HospitalType>>(HttpMethod.Get, "HospitalType/GetHospitalTypesAsync");
        }

        public async Task<HospitalType> InsertOrUpdateHospitalTypeAsync(HospitalType hospitalType)
        {
            return await _repository.SendAsync<HospitalType, HospitalType>(HttpMethod.Post, "HospitalType/InsertOrUpdateHospitalTypeAsync", hospitalType);
        }
    }
}
