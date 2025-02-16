using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class DoctorAppointmentDataManager : IDoctorAppointmentManager
    {
        private readonly ApplicationDBContext _dbContext;

        public DoctorAppointmentDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteDoctorAppointmentAsync(Guid appointmentId)
        {
            var appointment = await _dbContext.doctorAppointments.FindAsync(appointmentId);
            if (appointment == null)
            {
                return false;
            }

            _dbContext.doctorAppointments.Remove(appointment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> GenerateTokenNumberAsync(Guid hospitalId, Guid? doctorId, DateTime appointmentDate)
        {
            var highestToken = await _dbContext.doctorAppointments
                .Where(a => a.HospitalId == hospitalId && a.DoctorId == doctorId && a.AppointmentDate == appointmentDate.Date)
                .MaxAsync(a => (int?)a.TokenNo) ?? 0;

            return highestToken + 1;
        }

        public async Task<List<DoctorAppointment>> GetActiveDoctorAppointmentsAsync(Guid hospitalId)
        {
            return await _dbContext.doctorAppointments
                .Where(a => a.HospitalId == hospitalId && a.IsActive)
                .ToListAsync();
        }

        public async Task<DoctorAppointment> GetDoctorAppointmentByIdAsync(Guid appointmentId)
        {
            return await _dbContext.doctorAppointments.FindAsync(appointmentId);
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsAsync(Guid hospitalId, DateTime? dateTime)
        {
            return await _dbContext.doctorAppointments
                .Where(a => a.HospitalId == hospitalId && a.AppointmentDate == dateTime.Value.Date)
                .ToListAsync();
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByDateRangeAsync(Guid hospitalId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.doctorAppointments
                .Where(a => a.HospitalId == hospitalId && a.AppointmentDate >= startDate && a.AppointmentDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByDoctorAsync(Guid hospitalId, Guid? doctorId, DateTime? dateTime)
        {
            return await _dbContext.doctorAppointments
                .Where(a => a.HospitalId == hospitalId && a.DoctorId == doctorId && a.AppointmentDate == dateTime.Value.Date)
                .ToListAsync();
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByPatientAsync(Guid hospitalId, string patientName)
        {
            return await _dbContext.doctorAppointments
                .Where(a => a.HospitalId == hospitalId && a.PatientName.Contains(patientName))
                .ToListAsync();
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByPhoneAsync(Guid hospitalId, string? phone, DateTime? dateTime)
        {
            return await _dbContext.doctorAppointments
                .Where(a => a.HospitalId == hospitalId && a.PatientPhone == phone && a.AppointmentDate == dateTime.Value.Date)
                .ToListAsync();
        }

        public async Task<DoctorAppointment> InsertOrUpdateDoctorAppointmentAsync(DoctorAppointment doctorAppointment)
        {
            if (doctorAppointment.AppointmentId == Guid.Empty)
            {
                doctorAppointment.AppointmentId = Guid.NewGuid();
                await _dbContext.doctorAppointments.AddAsync(doctorAppointment);
            }
            else
            {
                _dbContext.doctorAppointments.Update(doctorAppointment);
            }

            await _dbContext.SaveChangesAsync();
            return doctorAppointment;
        }
    }
}
