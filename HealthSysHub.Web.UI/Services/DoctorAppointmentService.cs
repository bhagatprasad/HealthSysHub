using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class DoctorAppointmentService : IDoctorAppointmentService
    {
        private readonly IRepositoryFactory _repository;

        public DoctorAppointmentService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeleteDoctorAppointmentAsync(Guid appointmentId)
        {
            var uri = Path.Combine("DoctorAppointment/DeleteDoctorAppointmentAsync", appointmentId.ToString());
            return await _repository.SendAsync<bool>(HttpMethod.Delete, uri);
        }

        public async Task<int> GenerateTokenNumberAsync(Guid hospitalId, Guid? doctorId, DateTime appointmentDate)
        {
            var uri = Path.Combine("DoctorAppointment/GenerateTokenNumberAsync", hospitalId.ToString(), doctorId?.ToString(), appointmentDate.ToString("yyyy-MM-dd"));
            return await _repository.SendAsync<int>(HttpMethod.Get, uri);
        }

        public async Task<List<DoctorAppointment>> GetActiveDoctorAppointmentsAsync(Guid hospitalId)
        {
            var uri = Path.Combine("DoctorAppointment/GetActiveDoctorAppointmentsAsync", hospitalId.ToString());
            return await _repository.SendAsync<List<DoctorAppointment>>(HttpMethod.Get, uri);
        }

        public async Task<DoctorAppointment> GetDoctorAppointmentByIdAsync(Guid appointmentId)
        {
            var uri = Path.Combine("DoctorAppointment/GetDoctorAppointmentByIdAsync", appointmentId.ToString());
            return await _repository.SendAsync<DoctorAppointment>(HttpMethod.Get, uri);
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsAsync(Guid hospitalId, DateTime? dateTime)
        {
            var uri = Path.Combine("DoctorAppointment/GetDoctorAppointmentsAsync", hospitalId.ToString());
            if (dateTime.HasValue)
            {
                uri += $"?dateTime={dateTime.Value:yyyy-MM-dd}";
            }
            return await _repository.SendAsync<List<DoctorAppointment>>(HttpMethod.Get, uri);
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByDateRangeAsync(Guid hospitalId, DateTime startDate, DateTime endDate)
        {
            var uri = Path.Combine("DoctorAppointment/GetDoctorAppointmentsByDateRangeAsync", hospitalId.ToString(), startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            return await _repository.SendAsync<List<DoctorAppointment>>(HttpMethod.Get, uri);
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByDoctorAsync(Guid hospitalId, Guid? doctorId, DateTime? dateTime)
        {
            var uri = Path.Combine("DoctorAppointment/GetDoctorAppointmentsByDoctorAsync", hospitalId.ToString(), doctorId?.ToString());
            if (dateTime.HasValue)
            {
                uri += $"?dateTime={dateTime.Value:yyyy-MM-dd}";
            }
            return await _repository.SendAsync<List<DoctorAppointment>>(HttpMethod.Get, uri);
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByPatientAsync(Guid hospitalId, string patientName)
        {
            var uri = Path.Combine("DoctorAppointment/GetDoctorAppointmentsByPatientAsync", hospitalId.ToString(), patientName);
            return await _repository.SendAsync<List<DoctorAppointment>>(HttpMethod.Get, uri);
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByPhoneAsync(Guid hospitalId, string? phone, DateTime? dateTime)
        {
            var uri = Path.Combine("DoctorAppointment/GetDoctorAppointmentsByPhoneAsync", hospitalId.ToString(), phone);
            if (dateTime.HasValue)
            {
                uri += $"?dateTime={dateTime.Value:yyyy-MM-dd}";
            }
            return await _repository.SendAsync<List<DoctorAppointment>>(HttpMethod.Get, uri);
        }

        public async Task<DoctorAppointment> InsertOrUpdateDoctorAppointmentAsync(DoctorAppointment doctorAppointment)
        {
            var uri = "DoctorAppointment/InsertOrUpdateDoctorAppointment";
            return await _repository.SendAsync<DoctorAppointment, DoctorAppointment>(HttpMethod.Post, uri, doctorAppointment);
        }
    }
}
