using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.DataManagers
{
    public class BedDataManager : IBedManager
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger<BedDataManager> _logger;

        public BedDataManager(ApplicationDBContext dbContext, ILogger<BedDataManager> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Bed> GetBedByIdAsync(Guid bedId)
        {
            return await _dbContext.beds
                .FirstOrDefaultAsync(b => b.BedId == bedId);
        }

        public async Task<List<Bed>> GetBedsByRoomAsync(Guid roomId)
        {
            return await _dbContext.beds
                .Where(b => b.RoomId == roomId)
                .OrderBy(b => b.BedNumber)
                .ToListAsync();
        }

        public async Task<List<Bed>> GetAvailableBedsAsync(Guid? roomId = null, string? bedType = null)
        {
            var query = _dbContext.beds
                .Where(b => !b.IsOccupied && b.IsActive);

            if (roomId.HasValue)
            {
                query = query.Where(b => b.RoomId == roomId.Value);
            }

            if (!string.IsNullOrWhiteSpace(bedType))
            {
                query = query.Where(b => b.BedType == bedType);
            }

            return await query
                .OrderBy(b => b.BedNumber)
                .ToListAsync();
        }

        public async Task<Bed> CreateBedAsync(Bed bed)
        {
            try
            {
                bed.CreatedOn = DateTimeOffset.UtcNow;
                await _dbContext.beds.AddAsync(bed);
                await _dbContext.SaveChangesAsync();
                return bed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating bed");
                throw;
            }
        }

        public async Task<Bed> UpdateBedAsync(Guid bedId, Bed bed, Guid modifiedBy)
        {
            var existingBed = await _dbContext.beds.FindAsync(bedId);
            if (existingBed == null) return null;

            bool hasChanges = EntityUpdater.HasChanges(existingBed, bed,
                nameof(Bed.CreatedBy), nameof(Bed.CreatedOn));

            if (hasChanges)
            {
                bed.ModifiedOn = DateTimeOffset.UtcNow;
                bed.ModifiedBy = modifiedBy;
                EntityUpdater.UpdateProperties(existingBed, bed,
                    nameof(Bed.CreatedBy), nameof(Bed.CreatedOn));
            }

            await _dbContext.SaveChangesAsync();
            return existingBed;
        }

        public async Task<bool> UpdateBedOccupancyAsync(Guid bedId, bool isOccupied, Guid modifiedBy)
        {
            var bed = await _dbContext.beds.FindAsync(bedId);
            if (bed == null) return false;

            bed.IsOccupied = isOccupied;
            bed.ModifiedBy = modifiedBy;
            bed.ModifiedOn = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBedStatusAsync(Guid bedId, bool isActive, Guid modifiedBy)
        {
            var bed = await _dbContext.beds.FindAsync(bedId);
            if (bed == null) return false;

            bed.IsActive = isActive;
            bed.ModifiedBy = modifiedBy;
            bed.ModifiedOn = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
