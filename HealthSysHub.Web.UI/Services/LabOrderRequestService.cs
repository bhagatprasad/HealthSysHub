using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class LabOrderRequestService : ILabOrderRequestService
    {
        private readonly IRepositoryFactory _repository;

        public LabOrderRequestService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<LabOrderRequestDetails> InsertOrUpdateLabOrderRequestAsync(LabOrderRequestDetails labOrderRequestDetails)
        {
            return await _repository.SendAsync<LabOrderRequestDetails, LabOrderRequestDetails>(HttpMethod.Post, "LabOrderRequest/InsertOrUpdateLabOrderRequestAsync", labOrderRequestDetails);
        }
    }
}
