using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class RoomDataManager : IRoomManager
    {
        private readonly ApplicationDBContext _dbContext;

        public RoomDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            try
            {
                room.CreatedOn = DateTimeOffset.UtcNow;
                await _dbContext.rooms.AddAsync(room);
                await _dbContext.SaveChangesAsync();
                return room;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Room>> GetAllRoomsAsync(Guid hospitalId)
        {
            return await _dbContext.rooms
                .Where(r => r.HospitalId == hospitalId)
                .ToListAsync();
        }

        public async Task<List<Room>> GetAvailableRoomsAsync(Guid hospitalId, Guid? roomTypeId = null, int? floorNumber = null)
        {
            var query = _dbContext.rooms
                .Where(r => r.HospitalId == hospitalId && r.IsActive);

            if (roomTypeId.HasValue)
            {
                query = query.Where(r => r.RoomTypeId == roomTypeId.Value);
            }

            if (floorNumber.HasValue)
            {
                query = query.Where(r => r.FloorNumber == floorNumber.Value);
            }

            return await query
                .OrderBy(r => r.FloorNumber)
                .ThenBy(r => r.Wing)
                .ThenBy(r => r.RoomNumber)
                .ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(Guid roomId)
        {
            return await _dbContext.rooms
                .FirstOrDefaultAsync(r => r.RoomId == roomId);
        }

        public Task<List<Room>> GetRoomTypesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MarkRoomAsCleanedAsync(Guid roomId, Guid cleanedBy)
        {
            var room = await _dbContext.rooms.FindAsync(roomId);
            if (room == null) return false;

            room.LastCleaned = DateTimeOffset.UtcNow;
            room.ModifiedBy = cleanedBy;
            room.ModifiedOn = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Room> UpdateRoomAsync(Guid roomId, Room model, Guid modifiedBy)
        {
            var existingRoom = await _dbContext.rooms.FindAsync(roomId);
            if (existingRoom == null) return null;

            bool hasChanges = EntityUpdater.HasChanges(existingRoom, model,
                nameof(Room.CreatedBy), nameof(Room.CreatedOn));

            if (hasChanges)
            {
                model.ModifiedOn = DateTimeOffset.UtcNow;
                model.ModifiedBy = modifiedBy;
                EntityUpdater.UpdateProperties(existingRoom, model,
                    nameof(Room.CreatedBy), nameof(Room.CreatedOn));
            }

            await _dbContext.SaveChangesAsync();
            return existingRoom;
        }

        public async Task<bool> UpdateRoomStatusAsync(Room model, Guid modifiedBy)
        {
            var existingRoom = await _dbContext.rooms.FindAsync(model.RoomId);
            if (existingRoom == null) return false;

            existingRoom.IsActive = model.IsActive;
            existingRoom.ModifiedBy = modifiedBy;
            existingRoom.ModifiedOn = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
