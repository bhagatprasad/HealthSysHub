using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly IConsultationService _consultationService;
        private readonly INotyfService _notifyService;

        public ConsultationController(IConsultationService consultationService,
            INotyfService notifyService)
        {
            _consultationService = consultationService;
            _notifyService = notifyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetConsultationDetailsByAppointmentId(Guid appointmentId)
        {
            if (appointmentId == Guid.Empty)
            {
                _notifyService.Error("Invalid appointment ID.");
                return BadRequest(new { status = false, message = "Invalid appointment ID." });
            }

            try
            {
                var consultationDetails = await _consultationService.GetConsultationDetailsByAppointmentIdAsync(appointmentId);

                if (consultationDetails == null || !consultationDetails.Any())
                {
                    _notifyService.Warning("No consultation details found for the specified appointment ID.");
                    return Json(new { status = false, message = "No consultation details found." });
                }

                return Json(new { status = true, data = consultationDetails.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                _notifyService.Error("An error occurred while retrieving consultation details.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateConsultationDetails([FromBody] ConsultationDetails consultationDetails)
        {
            if (consultationDetails == null)
            {
                _notifyService.Error("Consultation details cannot be null.");
                return BadRequest(new { status = false, message = "Consultation details cannot be null." });
            }

            try
            {
                var response = await _consultationService.InsertOrUpdateConsultationDetailsAsync(consultationDetails);

                if (response == null)
                {
                    _notifyService.Error("Unable to save consultation details, please check and re-submit again.");
                    return Json(new { status = false, data = consultationDetails });
                }

                _notifyService.Success("Consultation details saved successfully.");
                return Json(new { status = true, data = response });
            }
            catch (Exception ex)
            {
                _notifyService.Error("An error occurred while saving consultation details.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
