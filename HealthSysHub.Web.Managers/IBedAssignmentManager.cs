using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IBedAssignmentManager
    {
        Task<List<BedAssignment>> GetBedAssignmentsAsync();
        Task<List<BedAssignment>> GetActiveBedAssignmentsAsync();
        Task<BedAssignment> GetBedAssignmentByIdAsync(Guid assignmentId);
        Task<List<BedAssignment>> GetBedAssignmentsByAdmissionIdAsync(Guid admissionId);
        Task<BedAssignment> InsertOrUpdateBedAssignmentAsync(BedAssignment bedAssignment);
        Task<List<BedAssignment>> GetBedAssignmentsByStatusAsync(string status);
        Task<List<BedAssignment>> GetBedAssignmentsByWardIdAsync(Guid wardId);
        Task<List<BedAssignment>> GetBedAssignmentsByDateRangeAsync(DateTimeOffset? startDate, DateTimeOffset? endDate);
        Task<BedAssignment> GetMostRecentBedAssignmentAsync(Guid bedId);
    }
}
