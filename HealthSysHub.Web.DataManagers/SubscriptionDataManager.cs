using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class SubscriptionDataManager : ISubscriptionManager
    {
        private readonly ApplicationDBContext _dbContext;

        public SubscriptionDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Subscription>> GetHospitalSubscriptionsAsync(Guid hospitalId)
        {
            return await _dbContext.subscriptions
                .Where(s => s.HospitalId == hospitalId)
                .ToListAsync();
        }

        public async Task<List<Subscription>> GetLabSubscriptionsAsync(Guid labId)
        {
            return await _dbContext.subscriptions
                .Where(s => s.LabId == labId)
                .ToListAsync();
        }

        public async Task<List<Subscription>> GetPharmacySubscriptionsAsync(Guid pharmacyId)
        {
            return await _dbContext.subscriptions
                .Where(s => s.PharmacyId == pharmacyId)
                .ToListAsync();
        }

        public async Task<List<Subscription>> GetSubscriptionsAsync()
        {
            return await _dbContext.subscriptions.ToListAsync();
        }

        public async Task<Subscription> InsertOrUpdateSubscriptionAsync(Subscription subscription)
        {
            if (subscription.SubscriptionId == Guid.Empty)
            {
                subscription.SubscriptionId = Guid.NewGuid();
                await _dbContext.subscriptions.AddAsync(subscription);
            }
            else
            {
                _dbContext.subscriptions.Update(subscription);
            }

            await _dbContext.SaveChangesAsync();
            return subscription;
        }
    }

}
