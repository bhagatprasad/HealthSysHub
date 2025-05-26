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
    public class LabStaffController : ControllerBase
    {
        private readonly ILabStaffManager _labStaffManager;

        public LabStaffController(ILabStaffManager labStaffManager)
        {
            _labStaffManager = labStaffManager;
        }

        [HttpPost]
        [Route("InsertOrUpdateLabStaffAsync")]
        public async Task<IActionResult> InsertOrUpdateLabStaffAsync([FromBody] LabStaff staff)
        {
            try
            {
                var response = await _labStaffManager.InsertOrUpdateLabStaffAsync(staff);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLabStaffByIdAsync/{staffId}")]
        public async Task<IActionResult> GetLabStaffByIdAsync(Guid staffId)
        {
            try
            {
                var response = await _labStaffManager.GetLabStaffByIdAsync(staffId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLabStaffAsync")]
        public async Task<IActionResult> GetLabStaffAsync([FromQuery] Guid? hospitalId, [FromQuery] Guid? labId)
        {
            try
            {
                var response = await _labStaffManager.GetLabStaffAsync(hospitalId, labId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLabStaffsAsync")]
        public async Task<IActionResult> GetLabStaffsAsync()
        {
            try
            {
                var response = await _labStaffManager.GetLabStaffsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
