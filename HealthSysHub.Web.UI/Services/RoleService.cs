﻿using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryFactory _repository;

        public RoleService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _repository.SendAsync<List<Role>>(HttpMethod.Get, "Role/GetRolesAsync");
        }

        public async Task<Role> GetRoleByIdAsync(Guid roleId)
        {
            var uri = Path.Combine("Role/GetRoleByIdAsync", roleId.ToString());
            return await _repository.SendAsync<Role>(HttpMethod.Get, uri);
        }

        public async Task<Role> InsertOrUpdateRoleAsync(Role role)
        {
            return await _repository.SendAsync<Role, Role>(HttpMethod.Post, "Role/InsertOrUpdateRoleAsync", role);
        }
    }
}
