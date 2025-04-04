using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;
        private readonly INotyfService _notyfService;

        public SpecializationController(ISpecializationService specializationService, INotyfService notyfService)
        {
            _specializationService = specializationService;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FetchSpecializations()
        {
            try
            {
                var response = await _specializationService.GetSpecializationsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateSpecialization([FromBody] Specialization specialization)
        {
            try
            {
                var response = await _specializationService.InsertOrUpdateSpecializationAsync(specialization);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}