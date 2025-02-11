using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync();

        Task<Role> InsertOrUpdateRoleAsync(Role role);

        Task<Role> GetRoleByIdAsync(Guid roleId);

    }
}
