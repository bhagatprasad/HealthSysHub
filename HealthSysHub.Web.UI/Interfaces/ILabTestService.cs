
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface ILabTestService
    {
        Task<LabTest> InsertOrUpdateLabTestAsync(LabTest labTest);
        Task<LabTest> GetLabTestByIdAsync(Guid testId);
        Task<List<LabTest>> GetLabTestsAsync();
    }
}
