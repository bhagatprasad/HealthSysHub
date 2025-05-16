using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyManager
    {
        Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy);
        Task<Pharmacy> GetPharmacyByIdAsync(Guid pharmacyId);
        Task<List<Pharmacy>> GetPharmaciesAsync(string searchString);
        Task<List<Pharmacy>> GetPharmaciesAsync();
    }
}
