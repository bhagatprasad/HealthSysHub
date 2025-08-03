using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class WardBedDataManager : IWardBedManager
    {
        private readonly ApplicationDBContext _dbContext;

        public WardBedDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WardBed>> GetAllBedsAsync()
        {
            return await _dbContext.wardBeds.ToListAsync();
        }

        public async Task<List<WardBed>> GetActiveBedsAsync()
        {
            return await _dbContext.wardBeds
                    .Where(b => b.IsActive)
                    .ToListAsync();
        }

        public async Task<WardBed> GetBedByIdAsync(Guid bedId)
        {
            return await _dbContext.wardBeds
                    .FirstOrDefaultAsync(b => b.BedId == bedId);
        }

        public async Task<List<WardBed>> GetBedsByWardIdAsync(Guid wardId)
        {
            return await _dbContext.wardBeds
                    .Where(b => b.WardId == wardId && b.IsActive)
                    .ToListAsync();
        }

        public async Task<List<WardBed>> GetBedsByStatusAsync(string status)
        {
            return await _dbContext.wardBeds
                    .Where(b => b.Status == status && b.IsActive)
                    .ToListAsync();
        }

        public async Task<List<WardBed>> GetBedsByTypeAsync(string bedType)
        {
            return await _dbContext.wardBeds
                    .Where(b => b.BedType == bedType && b.IsActive)
                    .ToListAsync();
        }

        public async Task<WardBed> InsertOrUpdateBedAsync(WardBed wardBed)
        {
            if (wardBed.BedId == Guid.Empty || !await _dbContext.wardBeds.AnyAsync(b => b.BedId == wardBed.BedId))
            {
                // Insert new bed
                wardBed.CreatedOn = DateTimeOffset.UtcNow;
                await _dbContext.wardBeds.AddAsync(wardBed);
            }
            else
            {
                // Update existing bed
                var existingBed = await _dbContext.wardBeds.FindAsync(wardBed.BedId);
                if (existingBed != null)
                {
                    existingBed.WardId = wardBed.WardId;
                    existingBed.BedNumber = wardBed.BedNumber;
                    existingBed.BedType = wardBed.BedType;
                    existingBed.Status = wardBed.Status;
                    existingBed.ModifiedBy = wardBed.ModifiedBy;
                    existingBed.ModifiedOn = DateTimeOffset.UtcNow;
                    existingBed.IsActive = wardBed.IsActive;
                }
            }

            await _dbContext.SaveChangesAsync();
            return wardBed;
        }

        public async Task<bool> DeleteBedAsync(Guid bedId)
        {
            var bed = await _dbContext.wardBeds.FindAsync(bedId);
            if (bed != null)
            {
                bed.IsActive = false; // Soft delete
                bed.ModifiedOn = DateTimeOffset.UtcNow;
                _dbContext.wardBeds.Update(bed);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> GetBedCountByWardAsync(Guid wardId)
        {
            return await _dbContext.wardBeds
                    .CountAsync(b => b.WardId == wardId && b.IsActive);
        }

        public async Task<int> GetAvailableBedsCountAsync(Guid wardId)
        {
            return await _dbContext.wardBeds
                    .CountAsync(b => b.WardId == wardId &&
                                  b.Status == "Available" &&
                                  b.IsActive);
        }
    }

}
