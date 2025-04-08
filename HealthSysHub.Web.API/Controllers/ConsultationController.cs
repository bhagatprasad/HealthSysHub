using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class ConsultationController : ControllerBase
    {
        private readonly IConsultationManager _consultationManager;
        public ConsultationController(IConsultationManager consultationManager)
        {
            _consultationManager = consultationManager;
        }

        [HttpGet]
        [Route("GetAllConsultationsAsync")]
        public async Task<IActionResult> GetAllConsultationsAsync()
        {
            try
            {
                var response = await _consultationManager.GetConsultationDetailsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetConsultationByAppointmentIdAsync/{appointmentId}")]
        public async Task<IActionResult> GetConsultationByAppointmentIdAsync(Guid appointmentId)
        {
            try
            {
                var response = await _consultationManager.GetConsultationDetailsByAppointmentIdAsync(appointmentId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetConsultationByConsultationIdAsync/{consultationId}")]
        public async Task<IActionResult> GetConsultationByConsultationIdAsync(Guid consultationId)
        {
            try
            {
                var response = await _consultationManager.GetConsultationDetailsByConsultationIdAsync(consultationId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetConsultationsByDoctorAsync/{doctorId}")]
        public async Task<IActionResult> GetConsultationsByDoctorAsync(Guid doctorId)
        {
            try
            {
                var response = await _consultationManager.GetConsultationDetailsByDoctorAsync(doctorId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetConsultationsByHospitalAsync/{hospitalId}")]
        public async Task<IActionResult> GetConsultationsByHospitalAsync(Guid hospitalId)
        {
            try
            {
                var response = await _consultationManager.GetConsultationDetailsByHospitalAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateConsultationDetailsAsync")]
        public async Task<IActionResult> InsertOrUpdateConsultationDetailsAsync(ConsultationDetails consultationDetails)
        {
            try
            {
                var response = await _consultationManager.InsertOrUpdateConsultationDetailsAsync(consultationDetails);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
