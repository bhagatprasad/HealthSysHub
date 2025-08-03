using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class BedAssignmentDataManager : IBedAssignmentManager
    {
        private readonly ApplicationDBContext _dbContext;

        public BedAssignmentDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BedAssignment>> GetActiveBedAssignmentsAsync()
        {
            return await _dbContext.bedAssignments
                .Where(ba => ba.IsActive)
                .ToListAsync();
        }

        public async Task<BedAssignment> GetBedAssignmentByIdAsync(Guid assignmentId)
        {
            return await _dbContext.bedAssignments
                .FirstOrDefaultAsync(ba => ba.AssignmentId == assignmentId);
        }

        public async Task<List<BedAssignment>> GetBedAssignmentsAsync()
        {
            return await _dbContext.bedAssignments.ToListAsync();
        }

        public async Task<List<BedAssignment>> GetBedAssignmentsByAdmissionIdAsync(Guid admissionId)
        {
            return await _dbContext.bedAssignments
                .Where(ba => ba.AdmissionId == admissionId && ba.IsActive)
                .ToListAsync();
        }

        public async Task<List<BedAssignment>> GetBedAssignmentsByDateRangeAsync(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            return await _dbContext.bedAssignments
                .Where(ba => ba.AssignedDate >= startDate && ba.AssignedDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<BedAssignment>> GetBedAssignmentsByStatusAsync(string status)
        {
            return await _dbContext.bedAssignments
                .Where(ba => ba.IsActive)
                .ToListAsync();
        }

        public async Task<List<BedAssignment>> GetBedAssignmentsByWardIdAsync(Guid wardId)
        {
            return await _dbContext.bedAssignments
                .Where(ba => ba.WardId == wardId && ba.IsActive)
                .ToListAsync();
        }

        public async Task<BedAssignment> GetMostRecentBedAssignmentAsync(Guid bedId)
        {
            return await _dbContext.bedAssignments
                .Where(ba => ba.BedId == bedId)
                .OrderByDescending(ba => ba.AssignedDate)
                .FirstOrDefaultAsync();
        }

        public async Task<BedAssignment> InsertOrUpdateBedAssignmentAsync(BedAssignment bedAssignment)
        {
            if (bedAssignment.AssignmentId == Guid.Empty)
            {
                // Insert new BedAssignment
                await _dbContext.bedAssignments.AddAsync(bedAssignment);
            }
            else
            {
                // Update existing BedAssignment
                var existingBedAssignment = await _dbContext.bedAssignments.FindAsync(bedAssignment.AssignmentId);

                if (existingBedAssignment != null)
                {
                    // Check for changes and update properties
                    bool hasChanges = EntityUpdater.HasChanges(existingBedAssignment, bedAssignment,
                        nameof(BedAssignment.CreatedBy),
                        nameof(BedAssignment.CreatedDate),
                        nameof(BedAssignment.ModifiedBy),
                        nameof(BedAssignment.ModifiedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingBedAssignment, bedAssignment,
                            nameof(BedAssignment.CreatedBy),
                            nameof(BedAssignment.CreatedDate),
                            nameof(BedAssignment.ModifiedBy),
                            nameof(BedAssignment.ModifiedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return bedAssignment;
        }

    }

}
