using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class HospitalTypeDataManager : IHospitalTypeManager
    {
        private readonly ApplicationDBContext _dbContext;

        public HospitalTypeDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HospitalType> GetHospitalTypeAsync(Guid hospitalTypeId)
        {
            return await _dbContext.hospitalTypes.FindAsync(hospitalTypeId);
        }

        public async Task<List<HospitalType>> GetHospitalTypesAsync()
        {
            return await _dbContext.hospitalTypes.ToListAsync();
        }

        public async Task<HospitalType> InsertOrUpdateHospitalTypeAsync(HospitalType hospitalType)
        {
            if (hospitalType.HospitalTypeId == Guid.Empty)
            {
                // Insert new HospitalType
                hospitalType.HospitalTypeId = Guid.NewGuid(); // Generate a new ID if it's not set
                await _dbContext.hospitalTypes.AddAsync(hospitalType);
            }
            else
            {
                // Update existing HospitalType
                var existingHospitalType = await _dbContext.hospitalTypes.FindAsync(hospitalType.HospitalTypeId);

                if (existingHospitalType != null)
                {
                    // Check for changes and update properties
                    bool hasChanges = EntityUpdater.HasChanges(existingHospitalType, hospitalType, nameof(HospitalType.CreatedBy), nameof(HospitalType.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingHospitalType, hospitalType, nameof(HospitalType.CreatedBy), nameof(HospitalType.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return hospitalType;
        }
    }
}
