using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.DBConfiguration;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{

    public class InpatientMedicationDataManager : IInpatientMedicationManager
    {
        private readonly ApplicationDBContext _dbContext;

        public InpatientMedicationDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteMedicationAsync(Guid medicationId)
        {
            var medication = await _dbContext.inpatientMedications.FindAsync(medicationId);
            if (medication != null)
            {
                medication.IsActive = false; // Soft delete
                _dbContext.inpatientMedications.Update(medication);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<InpatientMedication>> GetActiveMedicationsAsync()
        {
            return await _dbContext.inpatientMedications
                .Where(m => m.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientMedication>> GetAllMedicationsAsync()
        {
            return await _dbContext.inpatientMedications.ToListAsync();
        }

        public async Task<InpatientMedication> GetMedicationByIdAsync(Guid medicationId)
        {
            return await _dbContext.inpatientMedications
                .FirstOrDefaultAsync(m => m.MedicationId == medicationId);
        }

        public async Task<int> GetMedicationCountAsync()
        {
            return await _dbContext.inpatientMedications.CountAsync();
        }

        public async Task<List<InpatientMedication>> GetMedicationsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return await _dbContext.inpatientMedications
                .Where(m => m.StartDate >= startDate && m.EndDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<InpatientMedication>> GetMedicationsByDoctorIdAsync(Guid doctorId)
        {
            return await _dbContext.inpatientMedications
                .Where(m => m.DoctorId == doctorId && m.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientMedication>> GetMedicationsByFrequencyAsync(string frequency)
        {
            return await _dbContext.inpatientMedications
                .Where(m => m.Frequency == frequency && m.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientMedication>> GetMedicationsByInpatientAndStatusAsync(Guid inpatientId, string status)
        {
            return await _dbContext.inpatientMedications
                .Where(m => m.InpatientId == inpatientId && m.Status == status && m.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientMedication>> GetMedicationsByInpatientIdAsync(Guid inpatientId)
        {
            return await _dbContext.inpatientMedications
                .Where(m => m.InpatientId == inpatientId && m.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientMedication>> GetMedicationsByMedicineIdAsync(Guid medicineId)
        {
            return await _dbContext.inpatientMedications
                .Where(m => m.MedicineId == medicineId && m.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientMedication>> GetMedicationsByStatusAsync(string status)
        {
            return await _dbContext.inpatientMedications
                .Where(m => m.Status == status && m.IsActive)
                .ToListAsync();
        }

        public async Task<InpatientMedication> GetMostRecentMedicationAsync(Guid inpatientId)
        {
            return await _dbContext.inpatientMedications
                .Where(m => m.InpatientId == inpatientId)
                .OrderByDescending(m => m.StartDate)
                .FirstOrDefaultAsync();
        }

        public async Task<InpatientMedication> InsertOrUpdateMedicationAsync(InpatientMedication medication)
        {
            if (medication.MedicationId == Guid.Empty)
            {
                // Insert new medication
                await _dbContext.inpatientMedications.AddAsync(medication);
            }
            else
            {
                // Update existing medication
                var existingMedication = await _dbContext.inpatientMedications.FindAsync(medication.MedicationId);
                if (existingMedication != null)
                {
                    // Update properties, excluding certain fields
                    existingMedication.InpatientId = medication.InpatientId;
                    existingMedication.MedicineId = medication.MedicineId;
                    existingMedication.DoctorId = medication.DoctorId;
                    existingMedication.Dosage = medication.Dosage;
                    existingMedication.Frequency = medication.Frequency;
                    existingMedication.StartDate = medication.StartDate;
                    existingMedication.EndDate = medication.EndDate;
                    existingMedication.Status = medication.Status;
                    existingMedication.Notes = medication.Notes;
                    existingMedication.ModifiedBy = medication.ModifiedBy;
                    existingMedication.ModifiedOn = DateTimeOffset.UtcNow;
                    existingMedication.IsActive = medication.IsActive; // Update active status
                }
            }

            await _dbContext.SaveChangesAsync();
            return medication;
        }
    }

}
