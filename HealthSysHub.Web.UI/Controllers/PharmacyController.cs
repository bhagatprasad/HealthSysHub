using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly INotyfService _notyfService;
        public PharmacyController(IPharmacyService pharmacyService,
            INotyfService notyfService)
        {
            _pharmacyService = pharmacyService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPharmacies()
        {
            try
            {
                var response = await _pharmacyService.GetPharmaciesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPharmacyById(Guid pharmacyId)
        {
            try
            {
                var response = await _pharmacyService.GetPharmacyByIdAsync(pharmacyId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePharmacy([FromBody] Pharmacy pharmacy)
        {
            try
            {
                var response = await _pharmacyService.InsertOrUpdatePharmacyAsync(pharmacy);
                _notyfService.Success("Pharmacy details processed successful");
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
