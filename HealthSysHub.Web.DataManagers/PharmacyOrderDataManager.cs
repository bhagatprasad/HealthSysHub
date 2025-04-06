using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyOrderDataManager : IPharmacyOrderManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PharmacyOrderDataManager(ApplicationDBContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<PharmacyOrder> GetPharmacyOrderByIdAsync(Guid pharmacyOrderId)
        {
            return await _dbContext.pharmacyOrders.FindAsync(pharmacyOrderId);
        }

        public async Task<List<PharmacyOrder>> GetPharmacyOrdersAsync()
        {
           return await _dbContext.pharmacyOrders.ToListAsync();
        }

        public async Task<PharmacyOrder> InsertOrUpdatePharmacyOrderAsync(PharmacyOrder pharmacyOrder)
        {
            if (pharmacyOrder.PharmacyOrderId == Guid.Empty)
            {
                await _dbContext.pharmacyOrders.AddAsync(pharmacyOrder);
            }
            else
            {
                var existingPharmacyOrder = await _dbContext.pharmacyOrders.FindAsync(pharmacyOrder.PharmacyOrderId);

                if (existingPharmacyOrder != null)
                {
                    var hasChanges = EntityUpdater.HasChanges(existingPharmacyOrder, pharmacyOrder, nameof(PharmacyOrder.CreatedBy), nameof(PharmacyOrder.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPharmacyOrder, pharmacyOrder, nameof(PharmacyOrder.CreatedBy), nameof(PharmacyOrder.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return pharmacyOrder;
        }
    }
}
