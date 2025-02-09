using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IDepartmentManager
    {
        Task<Department> InsertOrUpdateDepartmentAsync(Department department);
        Task<Department> GetDepartmentByIdAsync(Guid departmentId);
        Task<List<Department>> GetDepartmentsAsync();
    }
}
