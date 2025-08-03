using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class BedAssignmentController : ControllerBase
    {
        private readonly IBedAssignmentManager _bedAssignmentManager;

        public BedAssignmentController(IBedAssignmentManager bedAssignmentManager)
        {
            _bedAssignmentManager = bedAssignmentManager;
        }

        [HttpGet]
        [Route("GetBedAssignmentsAsync")]
        public async Task<IActionResult> GetBedAssignmentsAsync()
        {
            try
            {
                var response = await _bedAssignmentManager.GetBedAssignmentsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActiveBedAssignmentsAsync")]
        public async Task<IActionResult> GetActiveBedAssignmentsAsync()
        {
            try
            {
                var response = await _bedAssignmentManager.GetActiveBedAssignmentsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedAssignmentByIdAsync/{assignmentId}")]
        public async Task<IActionResult> GetBedAssignmentByIdAsync(Guid assignmentId)
        {
            try
            {
                var response = await _bedAssignmentManager.GetBedAssignmentByIdAsync(assignmentId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedAssignmentsByAdmissionIdAsync/{admissionId}")]
        public async Task<IActionResult> GetBedAssignmentsByAdmissionIdAsync(Guid admissionId)
        {
            try
            {
                var response = await _bedAssignmentManager.GetBedAssignmentsByAdmissionIdAsync(admissionId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateBedAssignmentAsync")]
        public async Task<IActionResult> InsertOrUpdateBedAssignmentAsync(BedAssignment bedAssignment)
        {
            try
            {
                var response = await _bedAssignmentManager.InsertOrUpdateBedAssignmentAsync(bedAssignment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedAssignmentsByStatusAsync/{status}")]
        public async Task<IActionResult> GetBedAssignmentsByStatusAsync(string status)
        {
            try
            {
                var response = await _bedAssignmentManager.GetBedAssignmentsByStatusAsync(status);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedAssignmentsByWardIdAsync/{wardId}")]
        public async Task<IActionResult> GetBedAssignmentsByWardIdAsync(Guid wardId)
        {
            try
            {
                var response = await _bedAssignmentManager.GetBedAssignmentsByWardIdAsync(wardId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedAssignmentsByDateRangeAsync")]
        public async Task<IActionResult> GetBedAssignmentsByDateRangeAsync([FromQuery] DateTimeOffset? startDate, [FromQuery] DateTimeOffset? endDate)
        {
            try
            {
                var response = await _bedAssignmentManager.GetBedAssignmentsByDateRangeAsync(startDate, endDate);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMostRecentBedAssignmentAsync/{bedId}")]
        public async Task<IActionResult> GetMostRecentBedAssignmentAsync(Guid bedId)
        {
            try
            {
                var response = await _bedAssignmentManager.GetMostRecentBedAssignmentAsync(bedId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
