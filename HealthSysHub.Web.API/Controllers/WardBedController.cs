using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DataManagers;
using HealthSysHub.Web.DBConfiguration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class WardBedController : ControllerBase
    {
        private readonly IWardBedManager _wardBedManager;

        public WardBedController(IWardBedManager wardBedManager)
        {
            _wardBedManager = wardBedManager;
        }

        [HttpGet]
        [Route("GetAllBeds")]
        public async Task<IActionResult> GetAllBeds()
        {
            try
            {
                var beds = await _wardBedManager.GetAllBedsAsync();
                return Ok(beds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActiveBeds")]
        public async Task<IActionResult> GetActiveBeds()
        {
            try
            {
                var beds = await _wardBedManager.GetActiveBedsAsync();
                return Ok(beds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedById/{bedId}")]
        public async Task<IActionResult> GetBedById(Guid bedId)
        {
            try
            {
                var bed = await _wardBedManager.GetBedByIdAsync(bedId);
                if (bed == null)
                    return NotFound();

                return Ok(bed);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedsByWard/{wardId}")]
        public async Task<IActionResult> GetBedsByWard(Guid wardId)
        {
            try
            {
                var beds = await _wardBedManager.GetBedsByWardIdAsync(wardId);
                return Ok(beds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedsByStatus/{status}")]
        public async Task<IActionResult> GetBedsByStatus(string status)
        {
            try
            {
                var beds = await _wardBedManager.GetBedsByStatusAsync(status);
                return Ok(beds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedsByType/{bedType}")]
        public async Task<IActionResult> GetBedsByType(string bedType)
        {
            try
            {
                var beds = await _wardBedManager.GetBedsByTypeAsync(bedType);
                return Ok(beds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateOrUpdateBed")]
        public async Task<IActionResult> CreateOrUpdateBed([FromBody] WardBed wardBed)
        {
            try
            {
                var result = await _wardBedManager.InsertOrUpdateBedAsync(wardBed);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteBed/{bedId}")]
        public async Task<IActionResult> DeleteBed(Guid bedId)
        {
            try
            {
                var result = await _wardBedManager.DeleteBedAsync(bedId);
                if (result)
                    return NoContent();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedCountByWard/{wardId}")]
        public async Task<IActionResult> GetBedCountByWard(Guid wardId)
        {
            try
            {
                var count = await _wardBedManager.GetBedCountByWardAsync(wardId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAvailableBedsCount/{wardId}")]
        public async Task<IActionResult> GetAvailableBedsCount(Guid wardId)
        {
            try
            {
                var count = await _wardBedManager.GetAvailableBedsCountAsync(wardId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
