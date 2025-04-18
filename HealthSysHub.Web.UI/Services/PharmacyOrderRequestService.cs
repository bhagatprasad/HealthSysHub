using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class PharmacyOrderRequestService : IPharmacyOrderRequestService
    {
        private readonly IRepositoryFactory _repository;

        public PharmacyOrderRequestService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<PharmacyOrderRequestDetails> InsertOrUpdatePharmacyOrderRequestDetailsAsync(PharmacyOrderRequestDetails requestDetails)
        {
            return await _repository.SendAsync<PharmacyOrderRequestDetails, PharmacyOrderRequestDetails>(HttpMethod.Post, "PharmacyOrderRequest/InsertOrUpdatePharmacyOrderRequestAsync", requestDetails);

        }
    }
}
