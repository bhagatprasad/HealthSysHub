using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;

namespace HealthSysHub.Web.UI.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IRepositoryFactory _repository;

        public MedicineService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public async Task<Medicine> GetMedicineByIdAsync(Guid medicineId)
        {
            var uri = Path.Combine("Medicine/GetMedicineByIdAsync", medicineId.ToString());
            return await _repository.SendAsync<Medicine>(HttpMethod.Get, uri);
        }

        public async Task<List<Medicine>> GetMedicinesAsync()
        {
            return await _repository.SendAsync<List<Medicine>>(HttpMethod.Get, "Medicine/GetMedicinesAsync");
        }

        public async Task<Medicine> InsertOrUpdateMedicineAsync(Medicine medicine)
        {
            return await _repository.SendAsync<Medicine, Medicine>(HttpMethod.Post, "Medicine/InsertOrUpdateMedicineAsync", medicine);
        }
    }
}
