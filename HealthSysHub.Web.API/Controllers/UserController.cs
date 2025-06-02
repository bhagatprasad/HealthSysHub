using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        [Route("FetchUsersAsync/{hospitalId:guid?}")]
        public async Task<IActionResult> FetchUsersAsync(Guid? hospitalId)
        {
            try
            {
                var response = await _userManager.FetchUsersAsync(hospitalId);


                return Ok(response);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("ChangePasswordAsync")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePassword changePassword)
        {
            try
            {
                var response = await _userManager.ChangePasswordAsync(changePassword);


                return Ok(response);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("ProfileUpdateRequestAsync")]
        public async Task<IActionResult> ProfileUpdateRequestAsync(ProfileUpdateRequest profile)
        {
            try
            {
                var response = await _userManager.ProfileUpdateRequestAsync(profile);


                return Ok(response);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
