using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface ILabStaffManager
    {
        Task<LabStaff> InsertOrUpdateLabStaffAsync(LabStaff staff);
        Task<LabStaff> GetLabStaffByIdAsync(Guid staffId);
        Task<List<LabStaff>> GetLabStaffAsync(Guid? hospitalId, Guid? labId);
        Task<List<LabStaff>> GetLabStaffsAsync();
    }
}
