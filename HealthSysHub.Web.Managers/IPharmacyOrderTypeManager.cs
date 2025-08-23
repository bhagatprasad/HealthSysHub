using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyOrderTypeManager
    {
        Task<PharmacyOrderType> InsertOrUpdatePharmacyOrderTypeAsync(PharmacyOrderType pharmacyOrderType);
        Task<PharmacyOrderType> GetPharmacyOrderTypeByIdAsync(Guid pharmacyOrderTypeId);
        Task<List<PharmacyOrderType>> GetPharmacyOrderTypesAsync();
    }
}
