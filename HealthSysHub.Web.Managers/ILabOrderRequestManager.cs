using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.Managers
{
    public interface ILabOrderRequestManager
    {
        //Lab test request
        Task<LabOrderRequestDetails> InsertOrUpdateLabOrderRequestAsync(LabOrderRequestDetails labOrderRequestDetails);
    }
}
