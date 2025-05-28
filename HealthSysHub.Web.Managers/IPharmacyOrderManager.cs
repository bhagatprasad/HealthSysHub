using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyOrderManager
    {
        Task<PharmacyOrder> InsertOrUpdatePharmacyOrderAsync(PharmacyOrder pharmacyOrder);
        Task<PharmacyOrder> GetPharmacyOrderByIdAsync(Guid pharmacyOrderId);
        Task<List<PharmacyOrder>> GetPharmacyOrdersAsync();
        Task<List<PharmacyOrder>> GetPharmacyOrdersByPharmacyAsync(Guid pharmacyId);

        Task<List<PharmacyOrderDetails>> GetPharmacyOrdersListByPharmacyAsync(Guid pharmacyId);
        Task<PharmacyOrderDetails> GetPharmacyOrderByIdAsync(Guid pharmacyId, Guid pharmacyOrderId);
        Task<PharmacyOrdersProcessResponse> ProcessPharmacyOrdersRequestAsync(PharmacyOrdersProcessRequest request);
    }
}
