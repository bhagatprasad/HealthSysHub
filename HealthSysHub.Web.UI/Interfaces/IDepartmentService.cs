using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> InsertOrUpdateDepartmentAsync(Department department);
        Task<Department> GetDepartmentByIdAsync(Guid departmentId);
        Task<List<Department>> GetDepartmentsAsync();
    }
}
