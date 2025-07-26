using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class RoomTypeDataManager : IRoomTypeManager
    {
        private readonly ApplicationDBContext _dbContext;

        public RoomTypeDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RoomType> GetRoomTypeByIdAsync(Guid roomTypeId)
        {
            return await _dbContext.roomTypes.FindAsync(roomTypeId);
        }

        public async Task<List<RoomType>> GetRoomTypesAsync()
        {
            return await _dbContext.roomTypes.ToListAsync();
        }

        public async Task<List<RoomType>> GetRoomTypesAsync(string searchString)
        {
            var query = _dbContext.roomTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(rt =>
                    rt.TypeName != null && rt.TypeName.Contains(searchString) ||
                    rt.Description != null && rt.Description.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<RoomType> InsertOrUpdateRoomTypeAsync(RoomType roomType)
        {
            if (roomType.RoomTypeId == Guid.Empty)
            {
                roomType.CreatedOn = DateTimeOffset.UtcNow;
                await _dbContext.roomTypes.AddAsync(roomType);
            }
            else
            {
                var existingRoomType = await _dbContext.roomTypes.FindAsync(roomType.RoomTypeId);

                if (existingRoomType != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingRoomType, roomType,
                        nameof(RoomType.CreatedBy), nameof(RoomType.CreatedOn));

                    if (hasChanges)
                    {
                        roomType.ModifiedOn = DateTimeOffset.UtcNow;
                        EntityUpdater.UpdateProperties(existingRoomType, roomType,
                            nameof(RoomType.CreatedBy), nameof(RoomType.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
            return roomType;
        }
    }
}
