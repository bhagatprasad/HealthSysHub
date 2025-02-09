using HealthSysHub.Web.Managers;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class HospitalTypeFunctions
    {
        private readonly ILogger<HospitalTypeFunctions> _logger;
        private IHospitalTypeManager _hospitalTypeManager;
        public HospitalTypeFunctions(ILogger<HospitalTypeFunctions> logger, 
            IHospitalTypeManager hospitalTypeManager)
        {
            _logger = logger;
            _hospitalTypeManager = hospitalTypeManager;
        }
    }
}
