using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.Utility.Models;
using System.Collections.Generic;

namespace HealthSysHub.Web.UI.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IRepositoryFactory _repository;

        public HospitalService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<Hospital> GetHospitalByIdAsync(Guid hospitalId)
        {
            var uri = Path.Combine("Hospital/GetHospitalByIdAsync", hospitalId.ToString());
            return await _repository.SendAsync<Hospital>(HttpMethod.Get, uri);
        }

        public async Task<HospitalInformation> GetHospitalInformationByIdAsync(Guid hospitalId)
        {
            var uri = Path.Combine("Hospital/GetHospitalInformationByIdAsync", hospitalId.ToString());
            return await _repository.SendAsync<HospitalInformation>(HttpMethod.Get, uri);
        }

        public async Task<List<HospitalInformation>> GetHospitalInformationsAsync()
        {
            return await _repository.SendAsync<List<HospitalInformation>>(HttpMethod.Get, "Hospital/GetHospitalInformationsAsync");
        }

        public async Task<List<Hospital>> GetHospitalsAsync()
        {
            return await _repository.SendAsync<List<Hospital>>(HttpMethod.Get, "Hospital/GetHospitalsAsync");
        }

        public async Task<Hospital> InsertOrUpdateHospitalAsync(Hospital hospital)
        {
            return await _repository.SendAsync<Hospital, Hospital>(HttpMethod.Post, "Hospital/InsertOrUpdateHospitalAsync", hospital);
        }

        public async Task<List<HospitalContactInformation>> InsertOrUpdateHospitalContactInformationAsync(HospitalContactInformation hospitalContactInformation)
        {
            return await _repository.SendAsync<HospitalContactInformation, List<HospitalContactInformation>>(HttpMethod.Post, "Hospital/InsertOrUpdateHospitalContactInformationAsync", hospitalContactInformation);
        }

        public async Task<HospitalContentInformation> InsertOrUpdateHospitalContentInformationAsync(HospitalContentInformation hospitalContentInformation)
        {
            return await _repository.SendAsync<HospitalContentInformation, HospitalContentInformation>(HttpMethod.Post, "Hospital/InsertOrUpdateHospitalContentInformationAsync", hospitalContentInformation);
        }

        public async Task<HospitalDepartmentInformation> InsertOrUpdateHospitalDepartmentInformationAsync(HospitalDepartmentInformation hospitalDepartmentInformation)
        {
            return await _repository.SendAsync<HospitalDepartmentInformation, HospitalDepartmentInformation>(HttpMethod.Post, "Hospital/InsertOrUpdateHospitalDepartmentInformationAsync", hospitalDepartmentInformation);
        }

        public async Task<HospitalSpecialtyInformation> InsertOrUpdateHospitalSpecialtyInformationAsync(HospitalSpecialtyInformation hospitalSpecialtyInformation)
        {
            return await _repository.SendAsync<HospitalSpecialtyInformation, HospitalSpecialtyInformation>(HttpMethod.Post, "Hospital/InsertOrUpdateHospitalSpecialtyInformationAsync", hospitalSpecialtyInformation);
        }
    }
}
