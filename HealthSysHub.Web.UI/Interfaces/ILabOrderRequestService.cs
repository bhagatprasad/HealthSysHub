using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface ILabOrderRequestService
    {
        Task<LabOrderRequestDetails> InsertOrUpdateLabOrderRequestAsync(LabOrderRequestDetails labOrderRequestDetails);
    }
}
