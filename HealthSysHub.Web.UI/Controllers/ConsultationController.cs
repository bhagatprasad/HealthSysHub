using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly IConsultationService _consultationService;
        private readonly INotyfService _notyfService;

        public ConsultationController(IConsultationService consultationService,
            INotyfService notyfService)
        {
            _consultationService = consultationService;
            _notyfService = notyfService;
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
                _notyfService.Error("Invalid appointment ID.");
                return BadRequest(new { status = false, message = "Invalid appointment ID." });
            }

            try
            {
                var consultationDetails = await _consultationService.GetConsultationDetailsByAppointmentIdAsync(appointmentId);

                if (consultationDetails == null || !consultationDetails.Any())
                {
                    _notyfService.Warning("No consultation details found for the specified appointment ID.");
                    return Json(new { status = false, message = "No consultation details found." });
                }

                return Json(new { status = true, data = consultationDetails.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                _notyfService.Error("An error occurred while retrieving consultation details.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateConsultationDetails([FromBody] ConsultationDetails consultationDetails)
        {
            if (consultationDetails == null)
            {
                _notyfService.Error("Consultation details cannot be null.");
                return BadRequest(new { status = false, message = "Consultation details cannot be null." });
            }

            try
            {
                var response = await _consultationService.InsertOrUpdateConsultationDetailsAsync(consultationDetails);

                if (response == null)
                {
                    _notyfService.Error("Unable to save consultation details, please check and re-submit again.");
                    return Json(new { status = false, data = consultationDetails });
                }

                _notyfService.Success("Consultation details saved successfully.");
                return Json(new { status = true, data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error("An error occurred while saving consultation details.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
