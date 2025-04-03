using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class LabTestController : ControllerBase
    {
        private readonly ILabTestManager _labTestManager;

        public LabTestController(ILabTestManager labTestManager)
        {
            _labTestManager = labTestManager;
        }
        [HttpGet]
        [Route("GetLabTestsAsync")]
        public async Task<IActionResult> GetLabTestsAsync()
        {
            try
            {
                var response = await _labTestManager.GetLabTestsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLabTestByIdAsync/{labTestId}")]
        public async Task<IActionResult> GetLabTestByIdAsync(Guid labTestId)
        {
            try
            {
                var response = await _labTestManager.GetLabTestByIdAsync(labTestId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateLabTestAsync")]
        public async Task<IActionResult> InsertOrUpdateLabTestAsync(LabTest labTest)
        {
            try
            {
                var response = await _labTestManager.InsertOrUpdateLabTestAsync(labTest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
