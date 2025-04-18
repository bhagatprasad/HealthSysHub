using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Services;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class PharmacyOrderRequestController : Controller
    {
        private readonly IPharmacyOrderRequestService _pharmacyOrderRequestService;
        private readonly INotyfService _notyfService;
        public PharmacyOrderRequestController(IPharmacyOrderRequestService pharmacyOrderRequestService, INotyfService notyfService)
        {
            _pharmacyOrderRequestService = pharmacyOrderRequestService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePharmacyOrderRequestDetails([FromBody] PharmacyOrderRequestDetails pharmacyOrderRequestDetails)
        {
            try
            {
                var pharmacyOrderRequestDetailsResponse = await _pharmacyOrderRequestService.InsertOrUpdatePharmacyOrderRequestDetailsAsync(pharmacyOrderRequestDetails);

                _notyfService.Success("Successfully processed pharmcy request and its details");

                return Json(new { data = pharmacyOrderRequestDetailsResponse });
            }
            catch (Exception ex)
            {
                _notyfService.Error("An error occurred while updating pharmcy request details.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
