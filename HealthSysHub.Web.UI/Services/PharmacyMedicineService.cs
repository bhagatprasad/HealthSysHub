using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class PharmacyMedicineService : IPharmacyMedicineService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public PharmacyMedicineService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<List<PharmacyMedicine>> GetPharmacyMedicineAsync()
        {
            return await _repositoryFactory.SendAsync<List<PharmacyMedicine>>(HttpMethod.Get, "GetPharmacyMedicineAsync");
        }

        public async Task<List<PharmacyMedicine>> GetPharmacyMedicineAsync(Guid pharmacyId)
        {
            string url = Path.Combine("GetPharmacyMedicineAsync", pharmacyId.ToString());
            return await _repositoryFactory.SendAsync<List<PharmacyMedicine>>(HttpMethod.Get, url);
        }

        public async Task<PharmacyMedicine> GetPharmacyMedicineByMedicineIdAsync(Guid medicineId)
        {
            string url = Path.Combine("GetPharmacyMedicineByMedicineIdAsync", medicineId.ToString());
            return await _repositoryFactory.SendAsync<PharmacyMedicine>(HttpMethod.Get, url);
        }

        public async Task<PharmacyMedicine> InsertOrUpdatePharmacyMedicineAsync(PharmacyMedicine pharmacyMedicine)
        {
            return await _repositoryFactory.SendAsync<PharmacyMedicine, PharmacyMedicine>(HttpMethod.Post, "InsertOrUpdatePharmacyMedicineAsync", pharmacyMedicine);
        }
    }
}
