using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IRoleManager
    {
        Task<List<Role>> GetRolesAsync();
        Task<Role> GetRoleByIdAsync(Guid roleId);
        Task<Role> InsertOrUpdateRole(Role role);
    }
}
