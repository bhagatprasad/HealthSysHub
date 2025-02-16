using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    public class DoctorAppointmentController : Controller
    {
        private readonly IDoctorAppointmentService _doctorAppointmentService;
        private readonly INotyfService _notyfService;

        public DoctorAppointmentController(IDoctorAppointmentService doctorAppointmentService,
            INotyfService notyfService)
        {
            _doctorAppointmentService = doctorAppointmentService;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorAppointments(Guid hospitalId, DateTime? dateTime)
        {
            try
            {
                var response = await _doctorAppointmentService.GetDoctorAppointmentsAsync(hospitalId, dateTime);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorAppointmentById(Guid appointmentId)
        {
            try
            {
                var response = await _doctorAppointmentService.GetDoctorAppointmentByIdAsync(appointmentId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateDoctorAppointment([FromBody] DoctorAppointment doctorAppointment)
        {
            try
            {
                var response = await _doctorAppointmentService.InsertOrUpdateDoctorAppointmentAsync(doctorAppointment);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDoctorAppointmentAsync(Guid appointmentId)
        {
            try
            {
                var result = await _doctorAppointmentService.DeleteDoctorAppointmentAsync(appointmentId);
                if (result)
                {
                    return NoContent();
                }
                return NotFound(new { message = "Appointment not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveDoctorAppointments(Guid hospitalId)
        {
            try
            {
                var response = await _doctorAppointmentService.GetActiveDoctorAppointmentsAsync(hospitalId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorAppointmentsByDoctor(Guid hospitalId, Guid? doctorId, DateTime? dateTime)
        {
            try
            {
                var response = await _doctorAppointmentService.GetDoctorAppointmentsByDoctorAsync(hospitalId, doctorId, dateTime);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorAppointmentsByPatient(Guid hospitalId, string patientName)
        {
            try
            {
                var response = await _doctorAppointmentService.GetDoctorAppointmentsByPatientAsync(hospitalId, patientName);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorAppointmentsByPhone(Guid hospitalId, string? phone, DateTime? dateTime)
        {
            try
            {
                var response = await _doctorAppointmentService.GetDoctorAppointmentsByPhoneAsync(hospitalId, phone, dateTime);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorAppointmentsByDateRange(Guid hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var response = await _doctorAppointmentService.GetDoctorAppointmentsByDateRangeAsync(hospitalId, startDate, endDate);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
