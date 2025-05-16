
using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class LabDataManager : ILabManager
    {
        private readonly ApplicationDBContext _dbContext;

        public LabDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Lab> GetLabByIdAsync(Guid labId)
        {
            return await _dbContext.labs.FindAsync(labId);
        }

        public async Task<List<Lab>> GetLabsAsync()
        {
            return await _dbContext.labs.ToListAsync();
        }

        public async Task<List<Lab>> GetLabsAsync(string searchString)
        {
            var query = _dbContext.labs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(l =>
                    l.LabName != null && l.LabName.Contains(searchString) ||
                    l.Address != null && l.Address.Contains(searchString) ||
                    l.City != null && l.City.Contains(searchString) ||
                    l.State != null && l.State.Contains(searchString) ||
                    l.Country != null && l.Country.Contains(searchString) ||
                    l.PostalCode != null && l.PostalCode.Contains(searchString) ||
                    l.PhoneNumber != null && l.PhoneNumber.Contains(searchString) ||
                    l.Email != null && l.Email.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<Lab> InsertOrUpdateLabAsync(Lab lab)
        {
            if (lab.LabId == Guid.Empty)
            {
                await _dbContext.labs.AddAsync(lab);
            }
            else
            {
                var existingLab = await _dbContext.labs.FindAsync(lab.LabId);

                if (existingLab != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingLab, lab, nameof(Lab.CreatedBy), nameof(Lab.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingLab, lab, nameof(Lab.CreatedBy), nameof(Lab.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return lab;
        }
    }
}
