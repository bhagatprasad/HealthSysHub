using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class DoctorAppointmentController : ControllerBase
    {
        private readonly IDoctorAppointmentManager _doctorAppointmentManager;

        public DoctorAppointmentController(IDoctorAppointmentManager doctorAppointmentManager)
        {
            _doctorAppointmentManager = doctorAppointmentManager;
        }

        [HttpGet]
        [Route("GetDoctorAppointmentsAsync/{hospitalId}")]
        public async Task<IActionResult> GetDoctorAppointmentsAsync(Guid hospitalId, [FromQuery] DateTime? dateTime)
        {
            try
            {
                var response = await _doctorAppointmentManager.GetDoctorAppointmentsAsync(hospitalId, dateTime);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDoctorAppointmentByIdAsync/{appointmentId}")]
        public async Task<IActionResult> GetDoctorAppointmentByIdAsync(Guid appointmentId)
        {
            try
            {
                var response = await _doctorAppointmentManager.GetDoctorAppointmentByIdAsync(appointmentId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateDoctorAppointment")]
        public async Task<IActionResult> InsertOrUpdateDoctorAppointment(DoctorAppointment doctorAppointment)
        {
            try
            {
                var response = await _doctorAppointmentManager.InsertOrUpdateDoctorAppointmentAsync(doctorAppointment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteDoctorAppointmentAsync/{appointmentId}")]
        public async Task<IActionResult> DeleteDoctorAppointmentAsync(Guid appointmentId)
        {
            try
            {
                var result = await _doctorAppointmentManager.DeleteDoctorAppointmentAsync(appointmentId);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActiveDoctorAppointmentsAsync/{hospitalId}")]
        public async Task<IActionResult> GetActiveDoctorAppointmentsAsync(Guid hospitalId)
        {
            try
            {
                var response = await _doctorAppointmentManager.GetActiveDoctorAppointmentsAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDoctorAppointmentsByDoctorAsync/{hospitalId}/{doctorId}")]
        public async Task<IActionResult> GetDoctorAppointmentsByDoctorAsync(Guid hospitalId, Guid? doctorId, [FromQuery] DateTime? dateTime)
        {
            try
            {
                var response = await _doctorAppointmentManager.GetDoctorAppointmentsByDoctorAsync(hospitalId, doctorId, dateTime);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDoctorAppointmentsByPatientAsync/{hospitalId}/{patientName}")]
        public async Task<IActionResult> GetDoctorAppointmentsByPatientAsync(Guid hospitalId, string patientName)
        {
            try
            {
                var response = await _doctorAppointmentManager.GetDoctorAppointmentsByPatientAsync(hospitalId, patientName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDoctorAppointmentsByPhoneAsync/{hospitalId}/{phone}")]
        public async Task<IActionResult> GetDoctorAppointmentsByPhoneAsync(Guid hospitalId, string? phone, [FromQuery] DateTime? dateTime)
        {
            try
            {
                var response = await _doctorAppointmentManager.GetDoctorAppointmentsByPhoneAsync(hospitalId, phone, dateTime);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDoctorAppointmentsByDateRangeAsync/{hospitalId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetDoctorAppointmentsByDateRangeAsync(Guid hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var response = await _doctorAppointmentManager.GetDoctorAppointmentsByDateRangeAsync(hospitalId, startDate, endDate);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GenerateTokenNumberAsync/{hospitalId}/{doctorId}/{appointmentDate}")]
        public async Task<IActionResult> GenerateTokenNumberAsync(Guid hospitalId, Guid? doctorId, DateTime appointmentDate)
        {
            try
            {
                var tokenNumber = await _doctorAppointmentManager.GenerateTokenNumberAsync(hospitalId, doctorId, appointmentDate);
                return Ok(tokenNumber);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
