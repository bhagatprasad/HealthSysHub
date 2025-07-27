using HealthSysHub.Web.DBConfiguration.Models;


namespace HealthSysHub.Web.Managers
{
    public interface IBedManager
    {
        Task<Bed> GetBedByIdAsync(Guid bedId);
        Task<List<Bed>> GetBedsByRoomAsync(Guid roomId);
        Task<List<Bed>> GetAvailableBedsAsync(Guid? roomId = null, string? bedType = null);
        Task<Bed> CreateBedAsync(Bed bed);
        Task<Bed> UpdateBedAsync(Guid bedId, Bed bed, Guid modifiedBy);
        Task<bool> UpdateBedOccupancyAsync(Guid bedId, bool isOccupied, Guid modifiedBy);
        Task<bool> UpdateBedStatusAsync(Guid bedId, bool isActive, Guid modifiedBy);
    }
}
