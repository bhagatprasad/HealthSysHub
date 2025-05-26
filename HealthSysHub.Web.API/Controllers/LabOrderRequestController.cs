using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class LabOrderRequestController : ControllerBase
    {
        private readonly ILabOrderRequestManager _labOrderRequestManager;
        public LabOrderRequestController(ILabOrderRequestManager labOrderRequestManager)
        {
            _labOrderRequestManager = labOrderRequestManager;
        }
        [HttpPost]
        [Route("InsertOrUpdateLabOrderRequestAsync")]
        public async Task<IActionResult> InsertOrUpdateLabOrderRequestAsync(LabOrderRequestDetails labOrderRequestDetails)
        {
            try
            {
                var response = await _labOrderRequestManager.InsertOrUpdateLabOrderRequestAsync(labOrderRequestDetails);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
