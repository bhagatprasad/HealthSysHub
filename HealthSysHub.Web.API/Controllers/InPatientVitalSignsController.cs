using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class InPatientVitalSignsController : ControllerBase
    {
        private readonly IInpatientVitalSignsManager _inpatientVitalSignsManager;

        public InPatientVitalSignsController(IInpatientVitalSignsManager inpatientVitalSignsManager)
        {
            _inpatientVitalSignsManager = inpatientVitalSignsManager;
        }

        [HttpGet]
        [Route("GetAllVitalSignsAsync")]
        public async Task<IActionResult> GetAllVitalSignsAsync()
        {
            try
            {
                var response = await _inpatientVitalSignsManager.GetAllVitalSignsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetVitalSignsByInpatientIdAsync/{inpatientId}")]
        public async Task<IActionResult> GetVitalSignsByInpatientIdAsync(Guid inpatientId)
        {
            try
            {
                var response = await _inpatientVitalSignsManager.GetVitalSignsByInpatientIdAsync(inpatientId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetVitalSignByIdAsync/{vitalSignId}")]
        public async Task<IActionResult> GetVitalSignByIdAsync(Guid vitalSignId)
        {
            try
            {
                var response = await _inpatientVitalSignsManager.GetVitalSignByIdAsync(vitalSignId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateVitalSignAsync")]
        public async Task<IActionResult> InsertOrUpdateVitalSignAsync([FromBody] InpatientVitalSigns vitalSign)
        {
            try
            {
                var response = await _inpatientVitalSignsManager.InsertOrUpdateVitalSignAsync(vitalSign);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteVitalSignAsync/{vitalSignId}")]
        public async Task<IActionResult> DeleteVitalSignAsync(Guid vitalSignId)
        {
            try
            {
                var result = await _inpatientVitalSignsManager.DeleteVitalSignAsync(vitalSignId);
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
    }

}
