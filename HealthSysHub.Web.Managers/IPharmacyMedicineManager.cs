using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPharmacyMedicineManager
    {
        Task<List<PharmacyMedicine>> GetPharmacyMedicineAsync();
        Task<List<PharmacyMedicine>> GetPharmacyMedicineAsync(Guid pharmacyId);
        Task<PharmacyMedicine> GetPharmacyMedicineByMedicineIdAsync(Guid medicineId);
        Task<PharmacyMedicine> InsertOrUpdatePharmacyMedicineAsync(PharmacyMedicine pharmacyMedicine);
    }
}
