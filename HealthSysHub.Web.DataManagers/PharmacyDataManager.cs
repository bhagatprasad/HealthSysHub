using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyDataManager : IPharmacyManager
    {
        private readonly ApplicationDBContext _dbContext;

        public PharmacyDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pharmacy> GetPharmacyByIdAsync(Guid pharmacyId)
        {
            return await _dbContext.pharmacies.FindAsync(pharmacyId);
        }

        public async Task<List<Pharmacy>> GetPharmaciesAsync()
        {
            return await _dbContext.pharmacies.ToListAsync();
        }

        public async Task<List<Pharmacy>> GetPharmaciesAsync(string searchString)
        {
            var query = _dbContext.pharmacies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(p =>
                    p.PharmacyName != null && p.PharmacyName.Contains(searchString) ||
                    p.Address != null && p.Address.Contains(searchString) ||
                    p.City != null && p.City.Contains(searchString) ||
                    p.State != null && p.State.Contains(searchString) ||
                    p.Country != null && p.Country.Contains(searchString) ||
                    p.PostalCode != null && p.PostalCode.Contains(searchString) ||
                    p.PhoneNumber != null && p.PhoneNumber.Contains(searchString) ||
                    p.Email != null && p.Email.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy)
        {
            if (pharmacy.PharmacyId == Guid.Empty)
            {
                await _dbContext.pharmacies.AddAsync(pharmacy);
            }
            else
            {
                var existingPharmacy = await _dbContext.pharmacies.FindAsync(pharmacy.PharmacyId);

                if (existingPharmacy != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingPharmacy, pharmacy, nameof(Pharmacy.CreatedBy), nameof(Pharmacy.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPharmacy, pharmacy, nameof(Pharmacy.CreatedBy), nameof(Pharmacy.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return pharmacy;
        }
    }


}
