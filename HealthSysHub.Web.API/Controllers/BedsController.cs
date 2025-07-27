using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class BedsController : ControllerBase
    {
        private readonly IBedManager _bedManager;
        private readonly ILogger<BedsController> _logger;

        public BedsController(IBedManager bedManager, ILogger<BedsController> logger)
        {
            _bedManager = bedManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetBedByIdAsync/{bedId}")]
        public async Task<IActionResult> GetBedByIdAsync(Guid bedId)
        {
            try
            {
                var bed = await _bedManager.GetBedByIdAsync(bedId);
                if (bed == null) return NotFound();
                return Ok(bed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting bed with ID {bedId}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBedsByRoomAsync/{roomId}")]
        public async Task<IActionResult> GetBedsByRoomAsync(Guid roomId)
        {
            try
            {
                var beds = await _bedManager.GetBedsByRoomAsync(roomId);
                return Ok(beds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting beds for room {roomId}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAvailableBedsAsync")]
        public async Task<IActionResult> GetAvailableBedsAsync(
            [FromQuery] Guid? roomId = null,
            [FromQuery] string? bedType = null)
        {
            try
            {
                var beds = await _bedManager.GetAvailableBedsAsync(roomId, bedType);
                return Ok(beds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting available beds");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateBedAsync")]
        public async Task<IActionResult> CreateBedAsync(Bed bed)
        {
            try
            {
                var createdBed = await _bedManager.CreateBedAsync(bed);
                return Ok(createdBed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating bed");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateBedAsync/{bedId}")]
        public async Task<IActionResult> UpdateBedAsync(Guid bedId, Bed bed)
        {
            try
            {
                if (bedId != bed.BedId)
                {
                    return BadRequest("Bed ID mismatch");
                }

                var updatedBed = await _bedManager.UpdateBedAsync(bedId, bed, bed.ModifiedBy.Value);

                if (updatedBed == null) return NotFound();
                return Ok(updatedBed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating bed with ID {bedId}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateBedOccupancyAsync/{bedId}/{isOccupied}/{modifiedBy}")]
        public async Task<IActionResult> UpdateBedOccupancyAsync(
            Guid bedId,
           bool isOccupied,
           Guid modifiedBy)
        {
            try
            {
                var result = await _bedManager.UpdateBedOccupancyAsync(bedId, isOccupied, modifiedBy);

                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating occupancy for bed {bedId}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
     
    }
}
