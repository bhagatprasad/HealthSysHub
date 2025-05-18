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
    public class LabController : ControllerBase
    {
        private readonly ILabManager _labManager;

        public LabController(ILabManager labManager)
        {
            _labManager = labManager;
        }

        [HttpGet]
        [Route("GetLabsAsync")]
        public async Task<IActionResult> GetLabsAsync()
        {
            try
            {
                var response = await _labManager.GetLabsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLabByIdAsync/{labId}")]
        public async Task<IActionResult> GetLabByIdAsync(Guid labId)
        {
            try
            {
                var response = await _labManager.GetLabByIdAsync(labId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateLabAsync")]
        public async Task<IActionResult> InsertOrUpdateLabAsync(Lab lab)
        {
            try
            {
                var response = await _labManager.InsertOrUpdateLabAsync(lab);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLabsAsync/{searchString}")]
        public async Task<IActionResult> GetLabsAsync(string searchString)
        {
            try
            {
                var response = await _labManager.GetLabsAsync(searchString);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
