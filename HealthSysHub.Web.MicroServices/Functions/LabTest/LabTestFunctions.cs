using HealthSysHub.Web.Managers;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class LabTestFunctions
    {
        private readonly ILogger<LabTestFunctions> _logger;
        private ILabTestManager _labTesteManager;
        public LabTestFunctions(ILogger<LabTestFunctions> logger,
            ILabTestManager labTesteManager)
        {
            _logger = logger;
            _labTesteManager = labTesteManager;
        }
    }
}
