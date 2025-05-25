using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface ISubscriptionManager
    {
        Task<List<Subscription>> GetSubscriptionsAsync();
        Task<List<Subscription>> GetPharmacySubscriptionsAsync(Guid pharmacyId);
        Task<List<Subscription>> GetLabSubscriptionsAsync(Guid labId);
        Task<List<Subscription>> GetHospitalSubscriptionsAsync(Guid hospitalId);
        Task<Subscription> InsertOrUpdateSubscriptionAsync(Subscription subscription);
    }
}
