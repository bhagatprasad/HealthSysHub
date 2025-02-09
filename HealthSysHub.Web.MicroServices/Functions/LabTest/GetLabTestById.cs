using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class LabTestFunctions
    {

        [Function("GetLabTestById")]
        public async Task<IActionResult> GetLabTestById([HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "labtest/getlabtestbyid/{labtestId}")] HttpRequest req,
            Guid labtestId)
        {
            _logger.LogInformation("LabTestFunctions.GetLabTestById Invoked");
            try
            {
                var data = await _labTesteManager.GetLabTestByIdAsync(labtestId);
                return new OkObjectResult(data);
            }
            catch (Exception)
            {
                _logger.LogError("error while retrieving labtest by Id");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
