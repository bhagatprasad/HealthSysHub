using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class InpatientAdmissionDataManager : IInpatientAdmissionManager
    {
        private readonly ApplicationDBContext _dbContext;

        public InpatientAdmissionDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteAdmissionAsync(Guid admissionId)
        {
            var admission = await _dbContext.inpatientAdmissions.FindAsync(admissionId);
            if (admission != null)
            {
                admission.IsActive = false; // Soft delete
                _dbContext.inpatientAdmissions.Update(admission);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<InpatientAdmission>> GetActiveAdmissionsAsync()
        {
            return await _dbContext.inpatientAdmissions
                .Where(a => a.IsActive)
                .ToListAsync();
        }

        public async Task<InpatientAdmission> GetAdmissionByIdAsync(Guid admissionId)
        {
            return await _dbContext.inpatientAdmissions
                .FirstOrDefaultAsync(a => a.AdmissionId == admissionId);
        }

        public async Task<int> GetAdmissionCountAsync()
        {
            return await _dbContext.inpatientAdmissions.CountAsync();
        }

        public async Task<List<InpatientAdmission>> GetAllAdmissionsAsync()
        {
            return await _dbContext.inpatientAdmissions.ToListAsync();
        }

        public async Task<List<InpatientAdmission>> GetAdmissionsByAdmissionDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return await _dbContext.inpatientAdmissions
                .Where(a => a.AdmissionDate >= startDate && a.AdmissionDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<InpatientAdmission>> GetAdmissionsByAdmittingDoctorIdAsync(Guid doctorId)
        {
            return await _dbContext.inpatientAdmissions
                .Where(a => a.AdmittingDoctorId == doctorId && a.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientAdmission>> GetAdmissionsByDischargeDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return await _dbContext.inpatientAdmissions
                .Where(a => a.DischargeDate >= startDate && a.DischargeDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<InpatientAdmission>> GetAdmissionsByExpectedStayDurationAsync(int duration)
        {
            return await _dbContext.inpatientAdmissions
                .Where(a => a.ExpectedStayDuration == duration && a.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientAdmission>> GetAdmissionsByHospitalIdAsync(Guid hospitalId)
        {
            return await _dbContext.inpatientAdmissions
                .Where(a => a.HospitalId == hospitalId && a.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientAdmission>> GetAdmissionsByPatientIdAsync(Guid patientId)
        {
            return await _dbContext.inpatientAdmissions
                .Where(a => a.PatientId == patientId && a.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientAdmission>> GetAdmissionsByStatusAsync(string status)
        {
            return await _dbContext.inpatientAdmissions
                .Where(a => a.Status == status && a.IsActive)
                .ToListAsync();
        }

        public async Task<InpatientAdmission> GetMostRecentAdmissionAsync(Guid patientId)
        {
            return await _dbContext.inpatientAdmissions
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.AdmissionDate)
                .FirstOrDefaultAsync();
        }

        public async Task<InpatientAdmission> InsertOrUpdateAdmissionAsync(InpatientAdmission admission)
        {
            if (admission.AdmissionId == Guid.Empty)
            {
                // Insert new admission
                await _dbContext.inpatientAdmissions.AddAsync(admission);
            }
            else
            {
                // Update existing admission
                var existingAdmission = await _dbContext.inpatientAdmissions.FindAsync(admission.AdmissionId);
                if (existingAdmission != null)
                {
                    // Update properties, excluding certain fields
                    existingAdmission.PatientId = admission.PatientId;
                    existingAdmission.HospitalId = admission.HospitalId;
                    existingAdmission.AdmittingDoctorId = admission.AdmittingDoctorId;
                    existingAdmission.AdmissionDate = admission.AdmissionDate;
                    existingAdmission.AdmissionType = admission.AdmissionType;
                    existingAdmission.ReasonForAdmission = admission.ReasonForAdmission;
                    existingAdmission.ExpectedStayDuration = admission.ExpectedStayDuration;
                    existingAdmission.DischargeDate = admission.DischargeDate;
                    existingAdmission.DischargeStatus = admission.DischargeStatus;
                    existingAdmission.Status = admission.Status;
                    existingAdmission.ModifiedBy = admission.ModifiedBy;
                    existingAdmission.ModifiedOn = DateTimeOffset.UtcNow;
                    existingAdmission.IsActive = admission.IsActive; // Update active status
                }
            }

            await _dbContext.SaveChangesAsync();
            return admission;
        }
    }

}
