using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly IRepositoryFactory _repository;

        public ConsultationService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public async Task<List<ConsultationDetails>> GetConsultationDetailsAsync()
        {
            return await _repository.SendAsync<List<ConsultationDetails>>(HttpMethod.Get, "Consultation/GetAllConsultationsAsync");
        }

        public async Task<List<ConsultationDetails>> GetConsultationDetailsByAppointmentIdAsync(Guid appointmentId)
        {
            var uri = Path.Combine("Consultation/GetConsultationByAppointmentIdAsync", appointmentId.ToString());
            return await _repository.SendAsync<List<ConsultationDetails>>(HttpMethod.Get, uri);
        }

        public async Task<List<ConsultationDetails>> GetConsultationDetailsByConsultationIdAsync(Guid consultationId)
        {
            var uri = Path.Combine("Consultation/GetConsultationByConsultationIdAsync", consultationId.ToString());
            return await _repository.SendAsync<List<ConsultationDetails>>(HttpMethod.Get, uri);
        }

        public async Task<List<ConsultationDetails>> GetConsultationDetailsByDoctorAsync(Guid doctorId)
        {
            var uri = Path.Combine("Consultation/GetConsultationsByDoctorAsync", doctorId.ToString());
            return await _repository.SendAsync<List<ConsultationDetails>>(HttpMethod.Get, uri);
        }

        public async Task<List<ConsultationDetails>> GetConsultationDetailsByHospitalAsync(Guid hospitalId)
        {
            var uri = Path.Combine("Consultation/GetConsultationsByHospitalAsync", hospitalId.ToString());
            return await _repository.SendAsync<List<ConsultationDetails>>(HttpMethod.Get, uri);
        }

        public async Task<List<Consultation>> GetConsultationsAsync()
        {
            return await _repository.SendAsync<List<Consultation>>(HttpMethod.Get, "Consultation/GetConsultationsAsync");
        }

        public async Task<List<Consultation>> GetConsultationsByDoctorAsync(Guid doctorId)
        {
            var uri = Path.Combine("Consultation/GetConsultationsByDoctorAsync", doctorId.ToString());
            return await _repository.SendAsync<List<Consultation>>(HttpMethod.Get, uri);
        }

        public async Task<List<Consultation>> GetConsultationsByHospitalAsync(Guid hospitalId)
        {
            var uri = Path.Combine("Consultation/GetConsultationsByHospitalAsync", hospitalId.ToString());
            return await _repository.SendAsync<List<Consultation>>(HttpMethod.Get, uri);
        }

        public async Task<Consultation> InsertOrUpdateConsultationAsync(Consultation consultation)
        {
            return await _repository.SendAsync<Consultation, Consultation>(HttpMethod.Post, "Consultation/InsertOrUpdateConsultationAsync", consultation);
        }

        public async Task<ConsultationDetails> InsertOrUpdateConsultationDetailsAsync(ConsultationDetails consultationDetails)
        {
            return await _repository.SendAsync<ConsultationDetails, ConsultationDetails>(HttpMethod.Post, "Consultation/InsertOrUpdateConsultationDetailsAsync", consultationDetails);
        }
    }
}
