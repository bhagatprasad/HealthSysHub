using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IUserManager _userManager;
        public AccountController(IAuthenticationManager authenticationManager, IUserManager userManager)
        {
            _authenticationManager = authenticationManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("AuthenticateUserAsync")]
        public async Task<IActionResult> AuthenticateUserAsync(UserAuthentication authentication)
        {
            try
            {
                var response = await _authenticationManager.AuthenticateUserAsync(authentication);


                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("GenarateUserClaimsAsync")]
        public async Task<IActionResult> GenarateUserClaimsAsync(AuthResponse authentication)
        {
            try
            {
                var response = await _authenticationManager.GenarateUserClaimsAsync(authentication);


                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ActivateOrInActivateUserAsync")]
        public async Task<IActionResult> ActivateOrInActivateUserAsync(ActivateOrInActivateUser activateUser)
        {
            try
            {
                var response = await _userManager.ActivateOrInActivateUserAsync(activateUser);


                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ResetPasswordAsync")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPassword resetPassword)
        {
            try
            {
                var response = await _userManager.ResetPasswordAsync(resetPassword);


                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("ForgotPasswordAsync")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPassword forgotPassword)
        {
            try
            {
                var response = await _userManager.ForgotPasswordAsync(forgotPassword);


                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
