using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface ILabManager
    {
        Task<Lab> InsertOrUpdateLabAsync(Lab lab);
        Task<Lab> GetLabByIdAsync(Guid labId);
        Task<List<Lab>> GetLabsAsync(string searchString);
        Task<List<Lab>> GetLabsAsync();
    }
}
