using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IRoomTypeManager
    {
        Task<RoomType> InsertOrUpdateRoomTypeAsync(RoomType roomType);
        Task<RoomType> GetRoomTypeByIdAsync(Guid roomTypeId);
        Task<List<RoomType>> GetRoomTypesAsync(string searchString);
        Task<List<RoomType>> GetRoomTypesAsync();
    }
}
