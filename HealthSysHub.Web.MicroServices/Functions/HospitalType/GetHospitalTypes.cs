using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class HospitalTypeFunctions
    {

        [Function("GetHospitalTypes")]
        public async Task<IActionResult> GetHospitalTypes([HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "hospitaltype/gethospitaltypes")]
            HttpRequest req)
        {
            _logger.LogInformation("HospitalTypeFunctions.GetHospitalTypes Invoked");
            try
            {

                var data = await _hospitalTypeManager.GetHospitalTypesAsync();
                return new OkObjectResult(data);
            }
            catch (Exception)
            {
                _logger.LogError("error while retrieving hospitaltypes");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
