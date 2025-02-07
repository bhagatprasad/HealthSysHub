using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface ILabTestManager
    {
        Task<LabTest> InsertOrUpdateLabTestAsync(LabTest labTests);
        Task<LabTest> GetLabTestByIdAsync(Guid TestId);
        Task<List<LabTest>> GetLabTestsAsync();
    }
}
