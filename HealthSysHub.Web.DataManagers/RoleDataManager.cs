using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class RoleDataManager : IRoleManager
    {
        private readonly ApplicationDBContext _dbContext;
        public RoleDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Role> GetRoleByIdAsync(Guid roleId)
        {
            return await _dbContext.roles.FindAsync(roleId);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _dbContext.roles.ToListAsync();
        }

        public async Task<Role> InsertOrUpdateRole(Role role)
        {
            if (role.RoleId == Guid.Empty)
            {
                await _dbContext.roles.AddAsync(role);
            }
            else
            {
                var existingRole = await _dbContext.roles.FindAsync(role.RoleId);

                if (existingRole != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingRole, role, nameof(Role.CreatedBy), nameof(Role.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingRole, role, nameof(Role.CreatedBy), nameof(Role.CreatedOn));
                    }
                }
            }
            await _dbContext.SaveChangesAsync();

            return role;

        }
    }
}
