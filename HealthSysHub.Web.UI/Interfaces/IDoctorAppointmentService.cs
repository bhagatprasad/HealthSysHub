﻿using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IDoctorAppointmentService
    {
        Task<DoctorAppointment> InsertOrUpdateDoctorAppointmentAsync(DoctorAppointment doctorAppointment);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsAsync(Guid hospitalId, DateTime? dateTime);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsByDoctorAsync(Guid hospitalId, Guid? doctorId, DateTime? dateTime);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsByPatientAsync(Guid hospitalId, string patientName);
        Task<DoctorAppointment> GetDoctorAppointmentByIdAsync(Guid appointmentId);
        Task<bool> DeleteDoctorAppointmentAsync(Guid appointmentId);
        Task<List<DoctorAppointment>> GetActiveDoctorAppointmentsAsync(Guid hospitalId);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsByDateRangeAsync(Guid hospitalId, DateTime startDate, DateTime endDate);
        Task<int> GenerateTokenNumberAsync(Guid hospitalId, Guid? doctorId, DateTime appointmentDate);
        Task<List<DoctorAppointment>> GetDoctorAppointmentsByPhoneAsync(Guid hospitalId, string? phone, DateTime? dateTime);
        Task<List<DoctorAppointmentDetails>> GetAppointmentsReportAsync(Models.PrintAppointmentsReportRequest request);
    }
}
