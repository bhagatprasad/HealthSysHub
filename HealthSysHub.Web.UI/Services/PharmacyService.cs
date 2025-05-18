using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public PharmacyService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<List<Pharmacy>> GetPharmaciesAsync(string searchString)
        {
            string url = Path.Combine("GetPharmaciesAsync", searchString);
            return await _repositoryFactory.SendAsync<List<Pharmacy>>(HttpMethod.Get, url);
        }

        public async Task<List<Pharmacy>> GetPharmaciesAsync()
        {
            return await _repositoryFactory.SendAsync<List<Pharmacy>>(HttpMethod.Get, "GetPharmaciesAsync");
        }

        public async Task<Pharmacy> GetPharmacyByIdAsync(Guid pharmacyId)
        {
            string url = Path.Combine("GetPharmacyByIdAsync", pharmacyId.ToString());
            return await _repositoryFactory.SendAsync<Pharmacy>(HttpMethod.Get, url);
        }

        public async Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy)
        {
            return await _repositoryFactory.SendAsync<Pharmacy, Pharmacy>(HttpMethod.Post, "InsertOrUpdatePharmacyAsync", pharmacy);
        }
    }
}
