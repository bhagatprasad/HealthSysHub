using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Services;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class LabOrderRequestController : Controller
    {
        private readonly ILabOrderRequestService _labOrderRequestService;
        private readonly INotyfService _notyfService;
        public LabOrderRequestController(ILabOrderRequestService labOrderRequestService, INotyfService notyfService)
        {
            _labOrderRequestService = labOrderRequestService;
            _notyfService = notyfService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateLabOrderRequest([FromBody] LabOrderRequestDetails labOrderRequestDetails)
        {
            try
            {
                var labOrderRequestDetailsResponse = await _labOrderRequestService.InsertOrUpdateLabOrderRequestAsync(labOrderRequestDetails);

                _notyfService.Success("Successfully processed lab request and its details");
                return Json(new { data = labOrderRequestDetailsResponse });
            }
            catch (Exception ex)
            {
                _notyfService.Error("An error occurred while updating lab request details.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
