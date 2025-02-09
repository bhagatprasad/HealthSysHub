using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class HospitalTypeFunctions
    {
       
        [Function("InsertOrUpdateHospitalType")]
        public async Task<IActionResult> InsertOrUpdateHospitalType([HttpTrigger(AuthorizationLevel.Function,"post",
            Route = "hospitaltype/insertorupdatehospitaltype")] HttpRequest req)
        {
            _logger.LogInformation("HospitalTypeFunctions.InsertOrUpdateHospitalType Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid hospitaltype details NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult($"{nameof(InsertOrUpdateHospitalType)}");

                var hospitaltype = JsonConvert.DeserializeObject<HospitalType>(requestBody);

                if (hospitaltype == null)
                    return new BadRequestObjectResult("valid hospitaltype details NOT provided");

                var response = await _hospitalTypeManager.InsertOrUpdateHospitalTypeAsync(hospitaltype);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading hospitaltype details : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
