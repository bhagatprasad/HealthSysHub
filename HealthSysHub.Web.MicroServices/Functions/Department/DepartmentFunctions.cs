using HealthSysHub.Web.Managers;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class DepartmentFunctions
    {
        private readonly ILogger<DepartmentFunctions> _logger;
        private IDepartmentManager _departmentManager;
        public DepartmentFunctions(ILogger<DepartmentFunctions> logger, IDepartmentManager departmentManager)
        {
            _logger = logger;
            _departmentManager = departmentManager;
        }
    }
}
