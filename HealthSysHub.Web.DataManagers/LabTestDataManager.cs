using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class LabTestDataManager : ILabTestManager
    {
        private readonly ApplicationDBContext _dbContext;

        public LabTestDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LabTest> GetLabTestByIdAsync(Guid testId)
        {
            return await _dbContext.labTests.FindAsync(testId);
        }

        public async Task<List<LabTest>> GetLabTestsAsync()
        {
            return await _dbContext.labTests.ToListAsync();
        }

        public async Task<LabTest> InsertOrUpdateLabTestAsync(LabTest labTest)
        {
            if (labTest.TestId == Guid.Empty)
            {
                // Insert new LabTest
                await _dbContext.labTests.AddAsync(labTest);
            }
            else
            {
                // Update existing LabTest
                var existingLabTest = await _dbContext.labTests.FindAsync(labTest.TestId);

                if (existingLabTest != null)
                {
                    // Check for changes and update properties
                    bool hasChanges = EntityUpdater.HasChanges(existingLabTest, labTest, nameof(LabTest.CreatedBy), nameof(LabTest.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingLabTest, labTest, nameof(LabTest.CreatedBy), nameof(LabTest.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return labTest;
        }
    }
}
