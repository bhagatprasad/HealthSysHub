using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HealthSysHub.Web.MicroServices.Functions
{
    public partial class DepartmentFunctions
    {
        [Function("GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById([HttpTrigger(AuthorizationLevel.Function,
            "get", Route ="department/getdepartmentbyid/{departmentId}")] HttpRequest req,
            Guid departmentId)
        {
            _logger.LogInformation("DepartmentFunctions.GetDepartmentById Invoked");
            try
            {
                var data = await _departmentManager.GetDepartmentByIdAsync(departmentId);
                return new OkObjectResult(data);
            }
            catch (Exception)
            {
                _logger.LogError("error while retrieving department by Id");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
