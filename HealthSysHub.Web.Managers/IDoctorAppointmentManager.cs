using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Utility.Models;


namespace HealthSysHub.Web.Managers
{
    public interface IDoctorAppointmentManager
    {
        Task<int> GenerateTokenNumberAsync(Guid hospitalId, Guid? doctorId, DateTime appointmentDate);
        Task<DoctorAppointment> InsertOrUpdateDoctorAppointmentAsync(DoctorAppointment doctorAppointment);
        Task<bool> DeleteDoctorAppointmentAsync(Guid appointmentId);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsAsync(Guid hospitalId, DateTime? dateTime);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsByDoctorAsync(Guid hospitalId, Guid? doctorId, DateTime? dateTime);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsByPatientAsync(Guid hospitalId, string patientName);
        Task<DoctorAppointment> GetDoctorAppointmentByIdAsync(Guid appointmentId);
        Task<List<DoctorAppointment>> GetActiveDoctorAppointmentsAsync(Guid hospitalId);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsByDateRangeAsync(Guid hospitalId, DateTime startDate, DateTime endDate);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsByPhoneAsync(Guid hospitalId, string? phone, DateTime? dateTime);

        Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsAsync(Guid hospitalId, DateTime? dateTime);
        Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsByDoctorAsync(Guid hospitalId, Guid? doctorId, DateTime? dateTime);
        Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsByPatientAsync(Guid hospitalId, string patientName);
        Task<DoctorAppointmentDetails> GetDoctorAppointmentDetailsByIdAsync(Guid appointmentId);
        Task<List<DoctorAppointmentDetails>> GetActiveDoctorAppointmentDetailsAsync(Guid hospitalId);
        Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsByDateRangeAsync(Guid hospitalId, DateTime startDate, DateTime endDate);
        Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsByPhoneAsync(Guid hospitalId, string? phone, DateTime? dateTime);

        Task<List<DoctorAppointmentDetails>> GetAppointmentsReportAsync(PrintAppointmentsReportRequest request);
    }
}
