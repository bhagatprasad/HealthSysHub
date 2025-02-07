using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PatientTypeDataManager : IPatientTypeManager
    {
        private readonly ApplicationDBContext _dbContext;

        public PatientTypeDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PatientType> GetPatientTypeByIdAsync(Guid patientTypeId)
        {
            return await _dbContext.patientTypes.FindAsync(patientTypeId);
        }

        public async Task<List<PatientType>> GetPatientTypesAsync()
        {
            return await _dbContext.patientTypes.ToListAsync();
        }

        public async Task<PatientType> InsertOrUpdatePatientTypeAsync(PatientType patientType)
        {
            if (patientType.PatientTypeId == Guid.Empty)
            {
                // Insert new PatientType
                await _dbContext.patientTypes.AddAsync(patientType);
            }
            else
            {
                // Update existing PatientType
                var existingPatientType = await _dbContext.patientTypes.FindAsync(patientType.PatientTypeId);

                if (existingPatientType != null)
                {
                    // Check for changes and update properties
                    bool hasChanges = EntityUpdater.HasChanges(existingPatientType, patientType, nameof(PatientType.CreatedBy), nameof(PatientType.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPatientType, patientType, nameof(PatientType.CreatedBy), nameof(PatientType.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return patientType;
        }
    }
}
