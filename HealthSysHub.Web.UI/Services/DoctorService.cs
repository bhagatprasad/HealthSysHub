using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepositoryFactory _repository;

        public DoctorService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<Doctor> GetDoctorByIdAsync(Guid doctorId)
        {
            var uri = Path.Combine("Doctor/GetDoctorByIdAsync", doctorId.ToString());
            return await _repository.SendAsync<Doctor>(HttpMethod.Get, uri);
        }

        public async Task<List<Doctor>> GetDoctorsAsync(Guid hospitalId)
        {
            var uri = Path.Combine("Staff/GetHospitalDoctorsAsync", hospitalId.ToString());
            return await _repository.SendAsync<List<Doctor>>(HttpMethod.Get, uri);
        }

        public async Task<Doctor> InsertOrUpdateDoctorAsync(Doctor doctor)
        {
            return await _repository.SendAsync<Doctor, Doctor>(HttpMethod.Post, "Doctor/InsertOrUpdateDoctorAsync", doctor);
        }
    }
}
