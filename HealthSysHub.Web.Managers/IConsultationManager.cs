using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IConsultationManager
    {
        Task<Consultation> InsertOrUpdateConsultationAsync(Consultation consultation);
        Task<List<Consultation>> GetConsultationsAsync();
        Task<List<Consultation>> GetConsultationsByDoctorAsync(Guid doctorId);
        Task<List<Consultation>> GetConsultationsByHospitalAsync(Guid doctorId);
        Task<ConsultationDetails> InsertOrUpdateConsultationDetailsAsync(ConsultationDetails consultationDetails);
        Task<List<ConsultationDetails>> GetConsultationDetailsAsync();
        Task<List<ConsultationDetails>> GetConsultationDetailsByAppointmentIdAsync(Guid appointmentId);
        Task<List<ConsultationDetails>> GetConsultationDetailsByConsultationIdAsync(Guid consultationId);
        Task<List<ConsultationDetails>> GetConsultationDetailsByDoctorAsync(Guid doctorId);
        Task<List<ConsultationDetails>> GetConsultationDetailsByHospitalAsync(Guid hospitalId);
    }
}
