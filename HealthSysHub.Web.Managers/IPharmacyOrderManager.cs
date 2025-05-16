using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyOrderManager
    {
        Task<PharmacyOrder> InsertOrUpdatePharmacyOrderAsync(PharmacyOrder pharmacyOrder);
        Task<PharmacyOrder> GetPharmacyOrderByIdAsync(Guid pharmacyOrderId);
        Task<List<PharmacyOrder>> GetPharmacyOrdersAsync();
        Task<List<PharmacyOrder>> GetPharmacyOrdersByPharmacyAsync(Guid pharmacyId);
    }
}
