using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class LabTestFunctions
    {
       
        [Function("GetLabTests")]
        public async Task<IActionResult> GetLabTests([HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "labtest/getlabtests")] 
           HttpRequest req)
        {
            _logger.LogInformation("LabTestFunctions.GetLabTests Invoked");
            try
            {

                var data = await _labTesteManager.GetLabTestsAsync();
                return new OkObjectResult(data);
            }
            catch (Exception)
            {
                _logger.LogError("error while retrieving labtests");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
