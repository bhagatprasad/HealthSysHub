using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class PharmacyMedicineDataManager : IPharmacyMedicineManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PharmacyMedicineDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<PharmacyMedicine>> GetPharmacyMedicineAsync()
        {
            return await _dbContext.pharmacyMedicines.ToListAsync();
        }

        public async Task<List<PharmacyMedicine>> GetPharmacyMedicineAsync(Guid pharmacyId)
        {
            return await _dbContext.pharmacyMedicines.Where(x => x.PharmacyId == pharmacyId).ToListAsync();
        }

        public async Task<PharmacyMedicine> GetPharmacyMedicineByMedicineIdAsync(Guid medicineId)
        {
            return await _dbContext.pharmacyMedicines.Where(x => x.MedicineId == medicineId).FirstOrDefaultAsync();
        }

        public async Task<PharmacyMedicine> InsertOrUpdatePharmacyMedicineAsync(PharmacyMedicine pharmacyMedicine)
        {
            if (pharmacyMedicine.MedicineId == Guid.Empty)
            {
                await _dbContext.pharmacyMedicines.AddAsync(pharmacyMedicine);
            }
            else
            {
                var existingPharmacy = await _dbContext.pharmacyMedicines.FindAsync(pharmacyMedicine.MedicineId);

                if (existingPharmacy != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingPharmacy, pharmacyMedicine, nameof(PharmacyMedicine.CreatedBy), nameof(PharmacyMedicine.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingPharmacy, pharmacyMedicine, nameof(PharmacyMedicine.CreatedBy), nameof(PharmacyMedicine.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return pharmacyMedicine;
        }
    }
}
