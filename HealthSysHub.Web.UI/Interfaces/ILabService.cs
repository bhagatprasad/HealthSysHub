using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface ILabService
    {
        Task<Lab> InsertOrUpdateLabAsync(Lab lab);
        Task<Lab> GetLabByIdAsync(Guid labId);
        Task<List<Lab>> GetLabsAsync(string searchString);
        Task<List<Lab>> GetLabsAsync();
    }
}
