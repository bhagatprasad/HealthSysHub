using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IRepositoryFactory _repository;

        public DepartmentService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<Department> GetDepartmentByIdAsync(Guid departmentId)
        {
            var uri = Path.Combine("Department/GetDepartmentByIdAsync", departmentId.ToString());
            return await _repository.SendAsync<Department>(HttpMethod.Get, uri);
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _repository.SendAsync<List<Department>>(HttpMethod.Get, "Department/GetDepartmentsAsync");
        }

        public async Task<Department> InsertOrUpdateDepartmentAsync(Department department)
        {
            return await _repository.SendAsync<Department, Department>(HttpMethod.Post, "Department/InsertOrUpdateDepartmentAsync", department);
        }
    }
}
