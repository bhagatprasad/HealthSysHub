using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class PatientTypeService : IPatientTypeService
    {
        private readonly IRepositoryFactory _repository;

        public PatientTypeService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<PatientType> GetPatientTypeByIdAsync(Guid patientTypeId)
        {
            var uri = Path.Combine("PatientType/GetPatientTypeByIdAsync", patientTypeId.ToString());
            return await _repository.SendAsync<PatientType>(HttpMethod.Get, uri);
        }

        public async Task<List<PatientType>> GetPatientTypesAsync()
        {
            return await _repository.SendAsync<List<PatientType>>(HttpMethod.Get, "PatientType/GetPatientTypesAsync");
        }

        public async Task<PatientType> InsertOrUpdatePatientTypeAsync(PatientType patientType)
        {
            return await _repository.SendAsync<PatientType, PatientType>(HttpMethod.Post, "PatientType/InsertOrUpdatePatientTypeAsync", hospitalType);
        }
    }
}
