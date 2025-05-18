using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IPharmacyMedicineService
    {
        Task<List<PharmacyMedicine>> GetPharmacyMedicineAsync();
        Task<List<PharmacyMedicine>> GetPharmacyMedicineAsync(Guid pharmacyId);
        Task<PharmacyMedicine> GetPharmacyMedicineByMedicineIdAsync(Guid medicineId);
        Task<PharmacyMedicine> InsertOrUpdatePharmacyMedicineAsync(PharmacyMedicine pharmacyMedicine);
    }
}
