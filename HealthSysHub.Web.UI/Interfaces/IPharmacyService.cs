using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IPharmacyService
    {
        Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy);
        Task<Pharmacy> GetPharmacyByIdAsync(Guid pharmacyId);
        Task<List<Pharmacy>> GetPharmaciesAsync(string searchString);
        Task<List<Pharmacy>> GetPharmaciesAsync();
    }
}
