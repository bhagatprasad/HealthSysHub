using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IInpatientMedicationManager
    {
        Task<List<InpatientMedication>> GetAllMedicationsAsync(); // Retrieve all inpatient medications
        Task<List<InpatientMedication>> GetActiveMedicationsAsync(); // Retrieve all active inpatient medications
        Task<InpatientMedication> GetMedicationByIdAsync(Guid medicationId); // Retrieve a specific medication by ID
        Task<List<InpatientMedication>> GetMedicationsByInpatientIdAsync(Guid inpatientId); // Retrieve medications by inpatient ID
        Task<List<InpatientMedication>> GetMedicationsByDoctorIdAsync(Guid doctorId); // Retrieve medications prescribed by a specific doctor
        Task<List<InpatientMedication>> GetMedicationsByStatusAsync(string status); // Retrieve medications by status
        Task<List<InpatientMedication>> GetMedicationsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate); // Retrieve medications by date range
        Task<InpatientMedication> InsertOrUpdateMedicationAsync(InpatientMedication medication); // Insert a new medication or update an existing one
        Task<bool> DeleteMedicationAsync(Guid medicationId); // Delete a medication record by ID (soft delete)
        Task<int> GetMedicationCountAsync(); // Get the total count of medications
        Task<List<InpatientMedication>> GetMedicationsByMedicineIdAsync(Guid medicineId); // Retrieve medications by medicine ID
        Task<List<InpatientMedication>> GetMedicationsByInpatientAndStatusAsync(Guid inpatientId, string status); // Retrieve medications for a specific inpatient by status
        Task<List<InpatientMedication>> GetMedicationsByFrequencyAsync(string frequency); // Retrieve medications by frequency
        Task<InpatientMedication> GetMostRecentMedicationAsync(Guid inpatientId); // Retrieve the most recent medication for a specific inpatient
    }

}
