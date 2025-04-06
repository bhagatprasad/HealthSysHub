using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacistDataManager : IPharmacistManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PharmacistDataManager(ApplicationDBContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<Pharmacist> GetPharmacistByIdAsync(Guid pharmacistId)
        {
            return await _dbContext.pharmacists.FindAsync(pharmacistId);
        }

        public async Task<List<Pharmacist>> GetPharmacistsAsync()
        {
           return await _dbContext.pharmacists.ToListAsync();
        }

        public async Task<List<Pharmacist>> GetPharmacistsByHospitalAsync(Guid hospitalId)
        {
           return await _dbContext.pharmacists.Where(a => a.HospitalId == hospitalId && a.IsActive).ToListAsync();
        }

        public async Task<Pharmacist> InsertOrUpdatePharmacistAsync(Pharmacist pharmacist)
        {
            if (pharmacist.PharmacistId == Guid.Empty)
            {
                await _dbContext.pharmacists.AddAsync(pharmacist);
            }
            else
            {
                var existingConsultation = await _dbContext.pharmacists.FindAsync(pharmacist.PharmacistId);

                if (existingConsultation != null)
                {
                    var hasChanges = EntityUpdater.HasChanges(existingConsultation, pharmacist, nameof(Pharmacist.CreatedBy), nameof(Pharmacist.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingConsultation, pharmacist, nameof(Pharmacist.CreatedBy), nameof(Pharmacist.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return pharmacist;
        }
    }
}
