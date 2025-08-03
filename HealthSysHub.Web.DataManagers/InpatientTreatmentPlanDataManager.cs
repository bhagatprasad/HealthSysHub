using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.EntityFrameworkCore;


namespace HealthSysHub.Web.DataManagers
{
    public class InpatientTreatmentPlanDataManager : IInpatientTreatmentPlanManager
    {
        private readonly ApplicationDBContext _dbContext;

        public InpatientTreatmentPlanDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteTreatmentPlanAsync(Guid treatmentPlanId)
        {
            var treatmentPlan = await _dbContext.inpatientTreatmentPlans.FindAsync(treatmentPlanId);
            if (treatmentPlan != null)
            {
                treatmentPlan.IsActive = false; // Soft delete
                _dbContext.inpatientTreatmentPlans.Update(treatmentPlan);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<InpatientTreatmentPlan>> GetActiveTreatmentPlansAsync()
        {
            return await _dbContext.inpatientTreatmentPlans
                .Where(tp => tp.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientTreatmentPlan>> GetAllTreatmentPlansAsync()
        {
            return await _dbContext.inpatientTreatmentPlans.ToListAsync();
        }

        public async Task<InpatientTreatmentPlan> GetTreatmentPlanByIdAsync(Guid treatmentPlanId)
        {
            return await _dbContext.inpatientTreatmentPlans
                .FirstOrDefaultAsync(tp => tp.TreatmentPlanId == treatmentPlanId);
        }

        public async Task<int> GetTreatmentPlanCountAsync()
        {
            return await _dbContext.inpatientTreatmentPlans.CountAsync();
        }

        public async Task<List<InpatientTreatmentPlan>> GetTreatmentPlansByInpatientIdAsync(Guid inpatientId)
        {
            return await _dbContext.inpatientTreatmentPlans
                .Where(tp => tp.InpatientId == inpatientId && tp.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientTreatmentPlan>> GetTreatmentPlansByDoctorIdAsync(Guid doctorId)
        {
            return await _dbContext.inpatientTreatmentPlans
                .Where(tp => tp.DoctorId == doctorId && tp.IsActive)
                .ToListAsync();
        }

        public async Task<List<InpatientTreatmentPlan>> GetTreatmentPlansByStatusAsync(string status)
        {
            return await _dbContext.inpatientTreatmentPlans
                .Where(tp => tp.Status == status && tp.IsActive)
                .ToListAsync();
        }

        public async Task<InpatientTreatmentPlan> InsertOrUpdateTreatmentPlanAsync(InpatientTreatmentPlan treatmentPlan)
        {
            if (treatmentPlan.TreatmentPlanId == Guid.Empty)
            {
                // Insert new treatment plan
                await _dbContext.inpatientTreatmentPlans.AddAsync(treatmentPlan);
            }
            else
            {
                // Update existing treatment plan
                var existingTreatmentPlan = await _dbContext.inpatientTreatmentPlans.FindAsync(treatmentPlan.TreatmentPlanId);
                if (existingTreatmentPlan != null)
                {
                    // Update properties
                    existingTreatmentPlan.InpatientId = treatmentPlan.InpatientId;
                    existingTreatmentPlan.DoctorId = treatmentPlan.DoctorId;
                    existingTreatmentPlan.PlanDetails = treatmentPlan.PlanDetails;
                    existingTreatmentPlan.StartDate = treatmentPlan.StartDate;
                    existingTreatmentPlan.ExpectedEndDate = treatmentPlan.ExpectedEndDate;
                    existingTreatmentPlan.Status = treatmentPlan.Status;
                    existingTreatmentPlan.ModifiedBy = treatmentPlan.ModifiedBy;
                    existingTreatmentPlan.ModifiedOn = DateTimeOffset.UtcNow;
                    existingTreatmentPlan.IsActive = treatmentPlan.IsActive; // Update active status
                }
            }

            await _dbContext.SaveChangesAsync();
            return treatmentPlan;
        }
    }

}
