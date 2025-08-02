using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.EntityFrameworkCore;


namespace HealthSysHub.Web.DataManagers
{

    public class WardDataManager : IWardManager
    {
        private readonly ApplicationDBContext _dbContext;

        public WardDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteWardAsync(Guid wardId)
        {
            var ward = await _dbContext.wards.FindAsync(wardId);
            if (ward != null)
            {
                ward.IsActive = false; // Soft delete
                _dbContext.wards.Update(ward);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Ward>> GetActiveWardsAsync()
        {
            return await _dbContext.wards
                .Where(w => w.IsActive)
                .ToListAsync();
        }

        public async Task<List<Ward>> GetAllWardsAsync()
        {
            return await _dbContext.wards.ToListAsync();
        }

        public async Task<Ward> GetWardByIdAsync(Guid wardId)
        {
            return await _dbContext.wards
                .FirstOrDefaultAsync(w => w.WardId == wardId);
        }

        public async Task<int> GetWardCountAsync()
        {
            return await _dbContext.wards.CountAsync();
        }

        public async Task<List<Ward>> GetWardsByHospitalIdAsync(Guid hospitalId)
        {
            return await _dbContext.wards
                .Where(w => w.HospitalId == hospitalId && w.IsActive)
                .ToListAsync();
        }

        public async Task<Ward> InsertOrUpdateWardAsync(Ward ward)
        {
            if (ward.WardId == null)
            {
                // Insert new ward
                await _dbContext.wards.AddAsync(ward);
            }
            else
            {
                // Update existing ward
                var existingWard = await _dbContext.wards.FindAsync(ward.WardId);
                if (existingWard != null)
                {
                    // Update properties
                    existingWard.HospitalId = ward.HospitalId;
                    existingWard.WardName = ward.WardName;
                    existingWard.WardType = ward.WardType;
                    existingWard.Capacity = ward.Capacity;
                    existingWard.CurrentOccupancy = ward.CurrentOccupancy;
                    existingWard.Description = ward.Description;
                    existingWard.ModifiedBy = ward.ModifiedBy;
                    existingWard.ModifiedOn = DateTimeOffset.UtcNow;
                    existingWard.IsActive = ward.IsActive; // Update active status
                }
            }

            await _dbContext.SaveChangesAsync();
            return ward;
        }
    }

}
