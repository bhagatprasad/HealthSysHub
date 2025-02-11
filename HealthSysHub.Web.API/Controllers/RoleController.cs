using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;
        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetRolesAsync")]
        public async Task<IActionResult> GetRolesAsync()
        {
            try
            {
                var responce = await _roleManager.GetRolesAsync();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetRoleByIdAsync/{roleId}")]
        public async Task<IActionResult> GetRoleByIdAsync(Guid roleId)
        {
            try
            {
                var responce = await _roleManager.GetRoleByIdAsync(roleId);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateRoleAsync")]
        public async Task<IActionResult> InsertOrUpdateRoleAsync(Role role)
        {
            try
            {
                var responce = await _roleManager.InsertOrUpdateRole(role);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
