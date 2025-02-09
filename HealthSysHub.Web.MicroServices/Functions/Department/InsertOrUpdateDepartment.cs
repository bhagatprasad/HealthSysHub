using HealthSysHub.Web.DBConfiguration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class DepartmentFunctions
    {
        [Function("InsertOrUpdateDepartment")]
        public async Task<IActionResult> InsertOrUpdateDepartment([HttpTrigger(AuthorizationLevel.Function,
            "post",Route ="department/insertorupdatedepartment")] HttpRequest req)
        {
            _logger.LogInformation("DepartmentFunctions.InsertOrUpdateDepartment Invoked");
            try
            {
                if (req.Body == null)
                    return new BadRequestObjectResult("valid department details NOT provided");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                    return new BadRequestObjectResult($"{nameof(InsertOrUpdateDepartment)}");

                var department = JsonConvert.DeserializeObject<Department>(requestBody);

                if (department == null)
                    return new BadRequestObjectResult("valid department details NOT provided");

                var response = await _departmentManager.InsertOrUpdateDepartmentAsync(department);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading department details : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
