using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class HospitalTypeFunctions
    {
        
        [Function("GetHospitalTypeById")]
        public async Task<IActionResult> GetHospitalTypeById([HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "hospitaltype/gethospitaltypebyid/{hospitaltypeId}")] 
            HttpRequest req,
            Guid hospitaltypeId)
        {
            _logger.LogInformation("HospitalTypeFunctions.GetHospitalTypeById Invoked");
            try
            {
                var data = await _hospitalTypeManager.GetHospitalTypeAsync(hospitaltypeId);
                return new OkObjectResult(data);
            }
            catch (Exception)
            {
                _logger.LogError("error while retrieving hospitaltype by Id");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
