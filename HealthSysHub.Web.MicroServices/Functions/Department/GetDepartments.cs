using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class DepartmentFunctions
    {
        [Function("GetDepartments")]
        public async Task<IActionResult> GetDepartments([HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = "department/getdepartments")] HttpRequest req)
        {
            _logger.LogInformation("DepartmentFunctions.GetDepartments Invoked");

            try
            {
               
                var data = await _departmentManager.GetDepartmentsAsync();
                return new OkObjectResult(data);
            }
            catch (Exception)
            {
                _logger.LogError(" error while retrieving departments");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
