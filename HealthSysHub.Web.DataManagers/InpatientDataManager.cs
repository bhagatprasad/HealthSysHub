using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.EntityFrameworkCore;


namespace HealthSysHub.Web.DataManagers
{
    public class InpatientDataManager : IInpatientManager
    {
        private readonly ApplicationDBContext _dbContext;

        public InpatientDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteInpatientAsync(Guid inpatientId)
        {
            var inpatient = await _dbContext.inpatients.FindAsync(inpatientId);
            if (inpatient != null)
            {
                inpatient.IsActive = false; // Soft delete
                _dbContext.inpatients.Update(inpatient);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Inpatient>> GetActiveInpatientsAsync()
        {
            return await _dbContext.inpatients
                .Where(i => i.IsActive)
                .ToListAsync();
        }

        public async Task<Inpatient> GetInpatientByIdAsync(Guid inpatientId)
        {
            return await _dbContext.inpatients
                .FirstOrDefaultAsync(i => i.InpatientId == inpatientId);
        }

        public async Task<int> GetInpatientCountAsync()
        {
            return await _dbContext.inpatients.CountAsync();
        }

        public async Task<List<Inpatient>> GetInpatientsAsync()
        {
            return await _dbContext.inpatients.ToListAsync();
        }

        public async Task<List<Inpatient>> GetInpatientsByAdmissionDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return await _dbContext.inpatients
                .Where(i => i.AdmissionDate >= startDate && i.AdmissionDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<Inpatient>> GetInpatientsByAdmittingDoctorIdAsync(Guid doctorId)
        {
            return await _dbContext.inpatients
                .Where(i => i.AdmittingDoctorId == doctorId && i.IsActive)
                .ToListAsync();
        }

        public async Task<List<Inpatient>> GetInpatientsByDischargeDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return await _dbContext.inpatients
                .Where(i => i.DischargeDate >= startDate && i.DischargeDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<Inpatient>> GetInpatientsByExpectedStayDurationAsync(int duration)
        {
            return await _dbContext.inpatients
                .Where(i => i.ExpectedStayDuration == duration && i.IsActive)
                .ToListAsync();
        }

        public async Task<List<Inpatient>> GetInpatientsByHospitalIdAsync(Guid hospitalId)
        {
            return await _dbContext.inpatients
                .Where(i => i.HospitalId == hospitalId && i.IsActive)
                .ToListAsync();
        }

        public async Task<List<Inpatient>> GetInpatientsByPatientIdAsync(Guid patientId)
        {
            return await _dbContext.inpatients
                .Where(i => i.PatientId == patientId && i.IsActive)
                .ToListAsync();
        }

        public async Task<List<Inpatient>> GetInpatientsByStatusAsync(string status)
        {
            return await _dbContext.inpatients
                .Where(i => i.CurrentStatus == status && i.IsActive)
                .ToListAsync();
        }

        public async Task<List<Inpatient>> GetInpatientsByWardIdAsync(Guid wardId)
        {
            return await _dbContext.inpatients
                .Where(i => i.WardId == wardId && i.IsActive)
                .ToListAsync();
        }

        public async Task<Inpatient> GetMostRecentInpatientAsync(Guid patientId)
        {
            return await _dbContext.inpatients
                .Where(i => i.PatientId == patientId)
                .OrderByDescending(i => i.AdmissionDate)
                .FirstOrDefaultAsync();
        }

        public async Task<Inpatient> InsertOrUpdateInpatientAsync(Inpatient inpatient)
        {
            if (inpatient.InpatientId == Guid.Empty)
            {
                // Insert new inpatient
                await _dbContext.inpatients.AddAsync(inpatient);
            }
            else
            {
                // Update existing inpatient
                var existingInpatient = await _dbContext.inpatients.FindAsync(inpatient.InpatientId);
                if (existingInpatient != null)
                {
                    // Update properties, excluding certain fields
                    existingInpatient.PatientId = inpatient.PatientId;
                    existingInpatient.HospitalId = inpatient.HospitalId;
                    existingInpatient.AdmissionDate = inpatient.AdmissionDate;
                    existingInpatient.DischargeDate = inpatient.DischargeDate;
                    existingInpatient.WardId = inpatient.WardId;
                    existingInpatient.BedId = inpatient.BedId;
                    existingInpatient.AdmittingDoctorId = inpatient.AdmittingDoctorId;
                    existingInpatient.CurrentStatus = inpatient.CurrentStatus;
                    existingInpatient.ReasonForAdmission = inpatient.ReasonForAdmission;
                    existingInpatient.ExpectedStayDuration = inpatient.ExpectedStayDuration;
                    existingInpatient.ModifiedBy = inpatient.ModifiedBy;
                    existingInpatient.ModifiedOn = DateTimeOffset.UtcNow;
                    existingInpatient.IsActive = inpatient.IsActive; // Update active status
                }
            }

            await _dbContext.SaveChangesAsync();
            return inpatient;
        }
    }

}
