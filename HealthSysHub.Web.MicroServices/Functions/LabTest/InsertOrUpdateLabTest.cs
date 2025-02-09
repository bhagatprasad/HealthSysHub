using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class LabTestFunctions 
    {
        
        [Function("InsertOrUpdateLabTest")]
        public async Task<IActionResult> InsertOrUpdateLabTest([HttpTrigger(AuthorizationLevel.Function,
            "post",Route = "labtest/insertorupdatelabtest")] HttpRequest req)
        {
            _logger.LogInformation("LabTestFunctions.InsertOrUpdateLabTest Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid labtest details NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult($"{nameof(InsertOrUpdateLabTest)}");

                var labtest = JsonConvert.DeserializeObject<LabTest>(requestBody);

                if (labtest == null)
                    return new BadRequestObjectResult("valid labtest details NOT provided");

                var response = await _labTesteManager.InsertOrUpdateLabTestAsync(labtest);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading details details : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
