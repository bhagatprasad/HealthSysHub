using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyOrderTypeDataManager : IPharmacyOrderTypeManager
    {
        private readonly ApplicationDBContext _dbContext;

        public PharmacyOrderTypeDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PharmacyOrderType> GetPharmacyOrderTypeByIdAsync(Guid pharmacyOrderTypeId)
        {
            return await _dbContext.pharmacyOrderTypes.FindAsync(pharmacyOrderTypeId);
        }

        public async Task<List<PharmacyOrderType>> GetPharmacyOrderTypesAsync()
        {
            return await _dbContext.pharmacyOrderTypes.ToListAsync();
        }

        public async Task<PharmacyOrderType> InsertOrUpdatePharmacyOrderTypeAsync(PharmacyOrderType pharmacyOrderType)
        {
            if (pharmacyOrderType.PharmacyOrderTypeId == Guid.Empty)
            {
                // Insert new PharmacyOrderType
                pharmacyOrderType.PharmacyOrderTypeId = Guid.NewGuid();
                pharmacyOrderType.CreatedOn = DateTimeOffset.Now;
                await _dbContext.pharmacyOrderTypes.AddAsync(pharmacyOrderType);
            }
            else
            {
                // Update existing PharmacyOrderType
                var existingPharmacyOrderType = await _dbContext.pharmacyOrderTypes.FindAsync(pharmacyOrderType.PharmacyOrderTypeId);

                if (existingPharmacyOrderType != null)
                {
                    // Check for changes and update properties
                    bool hasChanges = EntityUpdater.HasChanges(existingPharmacyOrderType, pharmacyOrderType,
                        nameof(PharmacyOrderType.CreatedBy),
                        nameof(PharmacyOrderType.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPharmacyOrderType, pharmacyOrderType,
                            nameof(PharmacyOrderType.CreatedBy),
                            nameof(PharmacyOrderType.CreatedOn));

                        // Set ModifiedOn timestamp
                        existingPharmacyOrderType.ModifiedOn = DateTimeOffset.Now;
                    }
                }
                else
                {
                    throw new KeyNotFoundException($"PharmacyOrderType with ID {pharmacyOrderType.PharmacyOrderTypeId} not found");
                }
            }

            await _dbContext.SaveChangesAsync();
            return pharmacyOrderType;
        }
    }
}
