using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface ILabStaffService
    {
        Task<LabStaff> InsertOrUpdateLabStaffAsync(LabStaff staff);
        Task<LabStaff> GetLabStaffByIdAsync(Guid staffId);
        Task<List<LabStaff>> GetLabStaffAsync(Guid? hospitalId, Guid? labId);
        Task<List<LabStaff>> GetLabStaffsAsync();
    }
}
