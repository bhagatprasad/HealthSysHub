using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeManager _roomTypeManager;

        public RoomTypeController(IRoomTypeManager roomTypeManager)
        {
            _roomTypeManager = roomTypeManager;
        }

        [HttpGet]
        [Route("GetRoomTypesAsync")]
        public async Task<IActionResult> GetRoomTypesAsync()
        {
            try
            {
                var response = await _roomTypeManager.GetRoomTypesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetRoomTypeByIdAsync/{roomTypeId}")]
        public async Task<IActionResult> GetRoomTypeByIdAsync(Guid roomTypeId)
        {
            try
            {
                var response = await _roomTypeManager.GetRoomTypeByIdAsync(roomTypeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateRoomTypeAsync")]
        public async Task<IActionResult> InsertOrUpdateRoomTypeAsync(RoomType roomType)
        {
            try
            {
                var response = await _roomTypeManager.InsertOrUpdateRoomTypeAsync(roomType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetRoomTypesAsync/{searchString}")]
        public async Task<IActionResult> GetRoomTypesAsync(string searchString)
        {
            try
            {
                var response = await _roomTypeManager.GetRoomTypesAsync(searchString);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
