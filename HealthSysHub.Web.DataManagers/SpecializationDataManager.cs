using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class SpecializationDataManager : ISpecializationManager
    {
        private readonly ApplicationDBContext _dbContext;

        public SpecializationDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Specialization> GetSpecializationByIdAsync(Guid specializationId)
        {
            return await _dbContext.specializations.FindAsync(specializationId);
        }

        public async Task<List<Specialization>> GetSpecializationsAsync()
        {
            return await _dbContext.specializations.ToListAsync();
        }

        public async Task<Specialization> InsertOrUpdateSpecializationAsync(Specialization specialization)
        {
            if (specialization.SpecializationId == Guid.Empty)
            {
                // Insert new Specialization
                await _dbContext.specializations.AddAsync(specialization);
            }
            else
            {
                // Update existing Specialization
                var existingSpecialization = await _dbContext.specializations.FindAsync(specialization.SpecializationId);

                if (existingSpecialization != null)
                {
                    // Check for changes and update properties
                    bool hasChanges = EntityUpdater.HasChanges(existingSpecialization, specialization, nameof(Specialization.CreatedBy), nameof(Specialization.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingSpecialization, specialization, nameof(Specialization.CreatedBy), nameof(Specialization.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return specialization;
        }
    }
}
