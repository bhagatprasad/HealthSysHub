using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class DepartmentDataManager : IDepartmentManager
    {
        private readonly ApplicationDBContext _dbContext;

        public DepartmentDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Department> GetDepartmentByIdAsync(Guid departmentId)
        {
            return await _dbContext.departments.FindAsync(departmentId);
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _dbContext.departments.ToListAsync();
        }

        public async Task<Department> InsertOrUpdateDepartmentAsync(Department department)
        {
            if (department.DepartmentId == Guid.Empty)
            {
                // Insert new Department
                await _dbContext.departments.AddAsync(department);
            }
            else
            {
                // Update existing Department
                var existingDepartment = await _dbContext.departments.FindAsync(department.DepartmentId);

                if (existingDepartment != null)
                {
                    // Check for changes and update properties
                    bool hasChanges = EntityUpdater.HasChanges(existingDepartment, department, nameof(Department.CreatedBy), nameof(Department.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingDepartment, department, nameof(Department.CreatedBy), nameof(Department.CreatedOn));
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return department;
        }
    }
}
