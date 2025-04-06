using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PatientVitalDataManager : IPatientVitalManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PatientVitalDataManager(ApplicationDBContext dbContext) 
        {
            this._dbContext = dbContext;
        }
        public async Task<PatientVital> GetPatientVitalAsync(Guid vitalId)
        {
            return await _dbContext.patientVitals.FindAsync(vitalId);
        }

        public async Task<List<PatientVital>> GetPatientVitalByHospitalAsync(Guid hospitalId)
        {
            return await _dbContext.patientVitals.Where(a => a.HospitalId == hospitalId && a.IsActive).ToListAsync();
        }

        public async Task<List<PatientVital>> GetPatientVitalByPatientAsync(Guid patientId)
        {
            return await _dbContext.patientVitals.Where(a => a.PatientId == patientId && a.IsActive).ToListAsync();
        }

        public async Task<List<PatientVital>> GetPatientVitalsAsync()
        {
            return await _dbContext.patientVitals.ToListAsync();
        }

        public async Task<PatientVital> InsertOrUpdatePatientVitalAsync(PatientVital patientVital)
        {
            if (patientVital.VitalId == Guid.Empty)
            {
                await _dbContext.patientVitals.AddAsync(patientVital);
            }
            else
            {       
                var existingpatientVital = await _dbContext.patientVitals.FindAsync(patientVital.VitalId);

                if (existingpatientVital != null)
                {
                    var hasChanges = EntityUpdater.HasChanges(existingpatientVital, patientVital, nameof(PatientVital.CreatedBy), nameof(PatientVital.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingpatientVital, patientVital, nameof(PatientVital.CreatedBy), nameof(PatientVital.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return patientVital;
        }
    }
}
