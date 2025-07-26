using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomManager _roomManager;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomManager roomManager, ILogger<RoomController> logger)
        {
            _roomManager = roomManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllRoomsAsync/{hospitalId}")]
        public async Task<IActionResult> GetAllRoomsAsync(Guid hospitalId)
        {
            try
            {
                var response = await _roomManager.GetAllRoomsAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all rooms");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAvailableRoomsAsync/{hospitalId}")]
        public async Task<IActionResult> GetAvailableRoomsAsync(
            Guid hospitalId,
            [FromQuery] Guid? roomTypeId = null,
            [FromQuery] int? floorNumber = null)
        {
            try
            {
                var response = await _roomManager.GetAvailableRoomsAsync(hospitalId, roomTypeId, floorNumber);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting available rooms");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetRoomByIdAsync/{roomId}")]
        public async Task<IActionResult> GetRoomByIdAsync(Guid roomId)
        {
            try
            {
                var response = await _roomManager.GetRoomByIdAsync(roomId);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting room with ID {roomId}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateRoomAsync")]
        public async Task<IActionResult> CreateRoomAsync(Room room)
        {
            try
            {
                var createdBy = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                room.CreatedBy = createdBy;

                var response = await _roomManager.CreateRoomAsync(room);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating room");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateRoomAsync/{roomId}")]
        public async Task<IActionResult> UpdateRoomAsync(Guid roomId, Room room)
        {
            try
            {
                if (roomId != room.RoomId)
                {
                    return BadRequest("Room ID mismatch");
                }

                var response = await _roomManager.UpdateRoomAsync(roomId, room, room.ModifiedBy.Value);

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating room with ID {roomId}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateRoomStatusAsync/{roomId}")]
        public async Task<IActionResult> UpdateRoomStatusAsync(Guid roomId,  Room room)
        {
            try
            {
                if (roomId != room.RoomId)
                {
                    return BadRequest("Room ID mismatch");
                }

                var response = await _roomManager.UpdateRoomStatusAsync(room, room.ModifiedBy.Value);

                if (!response)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for room with ID {roomId}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("MarkRoomAsCleanedAsync/{roomId}/{cleanedBy}")]
        public async Task<IActionResult> MarkRoomAsCleanedAsync(Guid roomId,Guid cleanedBy)
        {
            try
            {
                var response = await _roomManager.MarkRoomAsCleanedAsync(roomId, cleanedBy);

                if (!response)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error marking room with ID {roomId} as cleaned");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
