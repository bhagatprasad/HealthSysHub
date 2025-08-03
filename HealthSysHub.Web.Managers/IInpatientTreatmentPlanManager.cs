using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IInpatientTreatmentPlanManager
    {
        Task<List<InpatientTreatmentPlan>> GetAllTreatmentPlansAsync(); // Retrieve all treatment plans
        Task<List<InpatientTreatmentPlan>> GetActiveTreatmentPlansAsync(); // Retrieve all active treatment plans
        Task<InpatientTreatmentPlan> GetTreatmentPlanByIdAsync(Guid treatmentPlanId); // Retrieve a specific treatment plan by ID
        Task<List<InpatientTreatmentPlan>> GetTreatmentPlansByInpatientIdAsync(Guid inpatientId); // Retrieve treatment plans by inpatient ID
        Task<List<InpatientTreatmentPlan>> GetTreatmentPlansByDoctorIdAsync(Guid doctorId); // Retrieve treatment plans by doctor ID
        Task<InpatientTreatmentPlan> InsertOrUpdateTreatmentPlanAsync(InpatientTreatmentPlan treatmentPlan); // Insert or update a treatment plan
        Task<bool> DeleteTreatmentPlanAsync(Guid treatmentPlanId); // Delete a treatment plan by ID (soft delete)
        Task<int> GetTreatmentPlanCountAsync(); // Get the total count of treatment plans
        Task<List<InpatientTreatmentPlan>> GetTreatmentPlansByStatusAsync(string status); // Retrieve treatment plans by status
    }

}
