using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IRoomManager
    {
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> GetRoomByIdAsync(Guid roomId);
        Task<List<Room>> GetAvailableRoomsAsync(Guid hospitalId, Guid? roomTypeId = null, int? floorNumber = null);
        Task<List<Room>> GetAllRoomsAsync(Guid hospitalId);
        Task<Room> UpdateRoomAsync(Guid roomId, Room model, Guid modifiedBy);
        Task<bool> UpdateRoomStatusAsync(Room model, Guid modifiedBy);
        Task<bool> MarkRoomAsCleanedAsync(Guid roomId, Guid cleanedBy);
        Task<List<Room>> GetRoomTypesAsync();
    }
}
