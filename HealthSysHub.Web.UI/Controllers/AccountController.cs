using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHospitalService _hospitalService;
        private readonly IStaffService _staffService;
        public AccountController(IAuthenticateService authenticateService,
            INotyfService notyfService,
            IHttpContextAccessor httpContextAccessor,
            IHospitalService hospitalService,
            IStaffService staffService)
        {
            _authenticateService = authenticateService;
            _notyfService = notyfService;
            _httpContextAccessor = httpContextAccessor;
            _hospitalService = hospitalService;
            _staffService = staffService;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Clear session data
                HttpContext.Session.Clear();

                // Clear all cookies
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

                // Sign out the user
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            }

            return View();

        }
        [HttpPost]
        public async Task<JsonResult> Login([FromBody] UserAuthentication authentication)
        {
            HospitalInformation hospitalInformation = new HospitalInformation();

            List<HospitalStaff> hospitalStaffDetails = new List<HospitalStaff>();


            try
            {
                var responce = await _authenticateService.AuthenticateUserAsync(authentication);
                //check the response here

                if (responce != null)
                {
                    if (!string.IsNullOrEmpty(responce.JwtToken))
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("AccessToken", responce.JwtToken);

                        var userClaimes = await _authenticateService.GenarateUserClaimsAsync(responce);

                        _httpContextAccessor.HttpContext.Session.SetString("ApplicationUser", JsonConvert.SerializeObject(userClaimes));

                        var claimsIdentity = UserPrincipal.GenarateUserPrincipal(userClaimes);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                           new ClaimsPrincipal(claimsIdentity),
                                                           new AuthenticationProperties
                                                           {
                                                               IsPersistent = true,
                                                               ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                                                           });

                        if (userClaimes.HospitalId.HasValue)
                        {
                            hospitalInformation = await _hospitalService.GetHospitalInformationByIdAsync(userClaimes.HospitalId.Value);

                            hospitalStaffDetails = await _staffService.GetAllHospitalStaffAsync(userClaimes.HospitalId.Value);
                        }

                        return Json(new { appUser = userClaimes, status = true, hospitalInformation = hospitalInformation, hospitalStaffDetails = hospitalStaffDetails });
                    }

                    _notyfService.Error(responce.StatusMessage);
                }
                else
                {
                    _notyfService.Error("Something went wrong");
                }

                return Json(new { appUser = default(object), status = false, hospitalInformation = hospitalInformation, hospitalStaffDetails = hospitalStaffDetails });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }

        }
        public async Task<IActionResult> Logout()
        {
            var appuser = _httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");
            HttpContext.Session.Remove("AccessToken");
            HttpContext.Session.Remove("ApplicationUser");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("allowCookies");
            return RedirectToAction("Login", "Account", null);
        }
    }
}
