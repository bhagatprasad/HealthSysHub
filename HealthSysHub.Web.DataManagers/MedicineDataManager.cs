using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class MedicineDataManager : IMedicineManager
    {
        private readonly ApplicationDBContext _dbContext;

        public MedicineDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Medicine> GetMedicineByIdAsync(Guid medicineId)
        {
            return await _dbContext.medicines.FindAsync(medicineId);
        }
        public async Task<List<Medicine>> GetMedicinesAsync()
        {
            var query = _dbContext.medicines.AsQueryable();

            return await query.ToListAsync();
        }
        public async Task<List<Medicine>> GetMedicinesAsync(string searchString)
        {
            var query = _dbContext.medicines.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(m =>
                    m.MedicineName != null && m.MedicineName.Contains(searchString) ||
                    m.GenericName != null && m.GenericName.Contains(searchString) ||
                    m.DosageForm != null && m.DosageForm.Contains(searchString) ||
                    m.Strength != null && m.Strength.Contains(searchString) ||
                    m.Manufacturer != null && m.Manufacturer.Contains(searchString) ||
                    m.BatchNumber != null && m.BatchNumber.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<Medicine> InsertOrUpdateMedicineAsync(Medicine medicine)
        {
            if (medicine.MedicineId == Guid.Empty)
            {
                await _dbContext.medicines.AddAsync(medicine);
            }
            else
            {
                var existingMedicine = await _dbContext.medicines.FindAsync(medicine.MedicineId);

                if (existingMedicine != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingMedicine, medicine, nameof(Medicine.CreatedBy), nameof(Medicine.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingMedicine, medicine, nameof(Medicine.CreatedBy), nameof(Medicine.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return medicine;
        }
    }
}
