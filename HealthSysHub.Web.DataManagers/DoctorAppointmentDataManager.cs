using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;
using System;

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


        public async Task<DoctorAppointmentDetails?> GetDoctorAppointmentDetailsByIdAsync(Guid appointmentId)
        {
            var query = from appointment in _dbContext.doctorAppointments
                        where appointment.AppointmentId == appointmentId
                        join doctor in _dbContext.doctors on appointment.DoctorId equals doctor.DoctorId into doctorInfo
                        from doctorJoinedInfo in doctorInfo.DefaultIfEmpty()
                        select new DoctorAppointmentDetails
                        {
                            AppointmentId = appointment.AppointmentId,
                            HospitalId = appointment.HospitalId,
                            DoctorId = appointment.DoctorId,
                            DoctorName = doctorJoinedInfo != null ? doctorJoinedInfo.FullName : null,
                            AppointmentDate = appointment.AppointmentDate,
                            AppointmentTime = appointment.AppointmentTime,
                            PatientName = appointment.PatientName,
                            PatientPhone = appointment.PatientPhone,
                            ComingFrom = appointment.ComingFrom,
                            HealthIssue = appointment.HealthIssue,
                            TokenNo = appointment.TokenNo,
                            Amount = appointment.Amount,
                            PaymentType = appointment.PaymentType,
                            PaymentReference = appointment.PaymentReference,
                            CreatedBy = appointment.CreatedBy,
                            CreatedOn = appointment.CreatedOn,
                            ModifiedBy = appointment.ModifiedBy,
                            ModifiedOn = appointment.ModifiedOn,
                            IsActive = appointment.IsActive
                        };

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsAsync(Guid hospitalId, DateTime? dateTime)
        {
            return await _dbContext.doctorAppointments.Where(a => a.HospitalId == hospitalId && a.AppointmentDate == dateTime.Value.Date).OrderByDescending(x => x.TokenNo).ToListAsync();
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByDateRangeAsync(Guid hospitalId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.doctorAppointments.Where(a => a.HospitalId == hospitalId && a.AppointmentDate >= startDate && a.AppointmentDate <= endDate).ToListAsync();
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByDoctorAsync(Guid hospitalId, Guid? doctorId, DateTime? dateTime)
        {
            return await _dbContext.doctorAppointments.Where(a => a.HospitalId == hospitalId && a.DoctorId == doctorId && a.AppointmentDate == dateTime.Value.Date).ToListAsync();
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByPatientAsync(Guid hospitalId, string patientName)
        {
            return await _dbContext.doctorAppointments.Where(a => a.HospitalId == hospitalId && a.PatientName.Contains(patientName)).ToListAsync();
        }

        public async Task<List<DoctorAppointment>> GetDoctorAppointmentsByPhoneAsync(Guid hospitalId, string? phone, DateTime? dateTime)
        {
            return await _dbContext.doctorAppointments
                .Where(a => a.HospitalId == hospitalId && a.PatientPhone == phone && a.AppointmentDate == dateTime.Value.Date)
                .ToListAsync();
        }

        public async Task<DoctorAppointment> InsertOrUpdateDoctorAppointmentAsync(DoctorAppointment doctorAppointment)
        {
            if (doctorAppointment.AppointmentId == Guid.Empty || doctorAppointment.AppointmentId == null)
            {
                doctorAppointment.AppointmentId = Guid.NewGuid();
                var maxTokenNo = await _dbContext.doctorAppointments.Where(a => a.AppointmentDate == doctorAppointment.AppointmentDate).MaxAsync(a => (int?)a.TokenNo) ?? 0;
                doctorAppointment.TokenNo = maxTokenNo + 1;
                await _dbContext.doctorAppointments.AddAsync(doctorAppointment);
            }
            else
            {
                var existingDoctorAppointment = await _dbContext.doctorAppointments.FindAsync(doctorAppointment.AppointmentId);

                if (existingDoctorAppointment != null)
                {
                    // Check for changes and update properties
                    bool hasChanges = EntityUpdater.HasChanges(existingDoctorAppointment, doctorAppointment, nameof(DoctorAppointment.CreatedBy), nameof(DoctorAppointment.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingDoctorAppointment, doctorAppointment, nameof(DoctorAppointment.CreatedBy), nameof(DoctorAppointment.CreatedOn));
                    }
                }
                else
                {
                    doctorAppointment.AppointmentId = Guid.NewGuid();
                    var maxTokenNo = await _dbContext.doctorAppointments.Where(a => a.AppointmentDate == doctorAppointment.AppointmentDate).MaxAsync(a => (int?)a.TokenNo) ?? 0;
                    doctorAppointment.TokenNo = maxTokenNo + 1;
                    await _dbContext.doctorAppointments.AddAsync(doctorAppointment);
                }
            }

            await _dbContext.SaveChangesAsync();
            return doctorAppointment;
        }

        public Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsAsync(Guid hospitalId, DateTime? dateTime)
        {
            return GetAppointmentDetailsAsync(hospitalId, date: dateTime);
        }

        public Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsByDateRangeAsync(
            Guid hospitalId, DateTime startDate, DateTime endDate)
        {
            return GetAppointmentDetailsAsync(hospitalId, startDate: startDate, endDate: endDate);
        }

        public Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsByDoctorAsync(
            Guid hospitalId, Guid? doctorId, DateTime? dateTime)
        {
            return GetAppointmentDetailsAsync(hospitalId, doctorId: doctorId, date: dateTime);
        }

        public Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsByPatientAsync(
            Guid hospitalId, string patientName)
        {
            return GetAppointmentDetailsAsync(hospitalId, patientName: patientName);
        }

        public Task<List<DoctorAppointmentDetails>> GetDoctorAppointmentDetailsByPhoneAsync(
            Guid hospitalId, string? phone, DateTime? dateTime)
        {
            return GetAppointmentDetailsAsync(hospitalId, phone: phone, date: dateTime);
        }

        public Task<List<DoctorAppointmentDetails>> GetActiveDoctorAppointmentDetailsAsync(Guid hospitalId)
        {
            return GetAppointmentDetailsAsync(hospitalId).ContinueWith(task => task.Result.Where(x => x.IsActive).ToList());
        }
        private async Task<List<DoctorAppointmentDetails>> GetAppointmentDetailsAsync(Guid hospitalId, Guid? doctorId = null, string? patientName = null, string? phone = null, DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = from appointment in _dbContext.doctorAppointments
                        where appointment.HospitalId == hospitalId
                           && (!doctorId.HasValue || appointment.DoctorId == doctorId.Value)
                           && (string.IsNullOrEmpty(patientName) || appointment.PatientName.Contains(patientName))
                           && (string.IsNullOrEmpty(phone) || appointment.PatientPhone.Contains(phone))
                           && (!date.HasValue || appointment.AppointmentDate == date.Value.Date)
                           && (!startDate.HasValue || appointment.AppointmentDate >= startDate.Value.Date)
                           && (!endDate.HasValue || appointment.AppointmentDate <= endDate.Value.Date)
                        join doctor in _dbContext.doctors on appointment.DoctorId equals doctor.DoctorId into doctorInfo
                        from doctorJoinedInfo in doctorInfo.DefaultIfEmpty()
                        select new DoctorAppointmentDetails
                        {
                            AppointmentId = appointment.AppointmentId,
                            HospitalId = appointment.HospitalId,
                            DoctorId = appointment.DoctorId,
                            DoctorName = doctorJoinedInfo != null ? doctorJoinedInfo.FullName : null,
                            AppointmentDate = appointment.AppointmentDate,
                            AppointmentTime = appointment.AppointmentTime,
                            PatientName = appointment.PatientName,
                            PatientPhone = appointment.PatientPhone,
                            ComingFrom = appointment.ComingFrom,
                            HealthIssue = appointment.HealthIssue,
                            TokenNo = appointment.TokenNo,
                            Amount = appointment.Amount,
                            Status = appointment.Status,
                            StatusMessage = appointment.StatusMessage,
                            PaymentType = appointment.PaymentType,
                            PaymentReference = appointment.PaymentReference,
                            CreatedBy = appointment.CreatedBy,
                            CreatedOn = appointment.CreatedOn,
                            ModifiedBy = appointment.ModifiedBy,
                            ModifiedOn = appointment.ModifiedOn,
                            IsActive = appointment.IsActive
                        };

            return await query.ToListAsync();
        }

        public async Task<List<DoctorAppointmentDetails>> GetAppointmentsReportAsync(PrintAppointmentsReportRequest request)
        {
            if (request.HospitalId == null)
            {
                throw new ArgumentNullException(nameof(request.HospitalId), "HospitalId is required");
            }

            var query = from appointment in _dbContext.doctorAppointments
                        where appointment.HospitalId == request.HospitalId
                           && appointment.IsActive
                           && (!request.FromDate.HasValue || appointment.AppointmentDate >= request.FromDate.Value.Date)
                           && (!request.ToDate.HasValue || appointment.AppointmentDate <= request.ToDate.Value.Date)
                           && (!request.DoctorId.HasValue || appointment.DoctorId == request.DoctorId)
                           && (string.IsNullOrEmpty(request.SearchStr)
                               || appointment.PatientName.Contains(request.SearchStr)
                               || appointment.PatientPhone.Contains(request.SearchStr))
                        join doctor in _dbContext.doctors on appointment.DoctorId equals doctor.DoctorId into doctorInfo
                        from doctorJoinedInfo in doctorInfo.DefaultIfEmpty()
                        select new DoctorAppointmentDetails
                        {
                            AppointmentId = appointment.AppointmentId,
                            HospitalId = appointment.HospitalId,
                            DoctorId = appointment.DoctorId,
                            DoctorName = doctorJoinedInfo != null ? doctorJoinedInfo.FullName : null,
                            AppointmentDate = appointment.AppointmentDate,
                            AppointmentTime = appointment.AppointmentTime,
                            PatientName = appointment.PatientName,
                            PatientPhone = appointment.PatientPhone,
                            ComingFrom = appointment.ComingFrom,
                            HealthIssue = appointment.HealthIssue,
                            TokenNo = appointment.TokenNo,
                            Amount = appointment.Amount,
                            PaymentType = appointment.PaymentType,
                            PaymentReference = appointment.PaymentReference,
                            Status = appointment.Status,
                            StatusMessage = appointment.StatusMessage,
                            CreatedBy = appointment.CreatedBy,
                            CreatedOn = appointment.CreatedOn,
                            ModifiedBy = appointment.ModifiedBy,
                            ModifiedOn = appointment.ModifiedOn,
                            IsActive = appointment.IsActive
                        };

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
