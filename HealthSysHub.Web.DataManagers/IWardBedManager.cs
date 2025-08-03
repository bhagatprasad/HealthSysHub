using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.DataManagers
{
    public interface IWardBedManager
    {
        Task<List<WardBed>> GetAllBedsAsync();
        Task<List<WardBed>> GetActiveBedsAsync();
        Task<WardBed> GetBedByIdAsync(Guid bedId);
        Task<List<WardBed>> GetBedsByWardIdAsync(Guid wardId);
        Task<List<WardBed>> GetBedsByStatusAsync(string status);
        Task<List<WardBed>> GetBedsByTypeAsync(string bedType);
        Task<WardBed> InsertOrUpdateBedAsync(WardBed wardBed);
        Task<bool> DeleteBedAsync(Guid bedId);
        Task<int> GetBedCountByWardAsync(Guid wardId);
        Task<int> GetAvailableBedsCountAsync(Guid wardId);
    }

}
