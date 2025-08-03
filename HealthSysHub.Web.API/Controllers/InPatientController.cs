using HealthSysHub.Web.API.CustomFilters;
using Microsoft.AspNetCore.Mvc;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class InPatientController : ControllerBase
    {
        private readonly IInpatientManager _inpatientManager;

        public InPatientController(IInpatientManager inpatientManager)
        {
            _inpatientManager = inpatientManager;
        }

        [HttpGet]
        [Route("GetInPatientsAsync/{hospitalId}")]
        public async Task<IActionResult> GetInPatientsAsync(Guid hospitalId)
        {
            try
            {
                var response = await _inpatientManager.GetInpatientsByHospitalIdAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActiveInPatientsAsync")]
        public async Task<IActionResult> GetActiveInPatientsAsync()
        {
            try
            {
                var response = await _inpatientManager.GetActiveInpatientsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetInPatientByIdAsync/{inpatientId}")]
        public async Task<IActionResult> GetInPatientByIdAsync(Guid inpatientId)
        {
            try
            {
                var response = await _inpatientManager.GetInpatientByIdAsync(inpatientId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateInpatientAsync")]
        public async Task<IActionResult> InsertOrUpdateInpatientAsync([FromBody] Inpatient inpatient)
        {
            try
            {
                var response = await _inpatientManager.InsertOrUpdateInpatientAsync(inpatient);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteInpatientAsync/{inpatientId}")]
        public async Task<IActionResult> DeleteInpatientAsync(Guid inpatientId)
        {
            try
            {
                var result = await _inpatientManager.DeleteInpatientAsync(inpatientId);
                if (result)
                {
                    return NoContent(); // 204 No Content
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetInpatientsByStatusAsync/{status}")]
        public async Task<IActionResult> GetInpatientsByStatusAsync(string status)
        {
            try
            {
                var response = await _inpatientManager.GetInpatientsByStatusAsync(status);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Additional methods can be added here as needed
    }

}
