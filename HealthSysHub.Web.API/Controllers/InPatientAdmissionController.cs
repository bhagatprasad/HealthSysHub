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
    public class InPatientAdmissionController : ControllerBase
    {
        private readonly IInpatientAdmissionManager _inpatientAdmissionManager;

        public InPatientAdmissionController(IInpatientAdmissionManager inpatientAdmissionManager)
        {
            _inpatientAdmissionManager = inpatientAdmissionManager;
        }

        [HttpGet]
        [Route("GetAllAdmissionsAsync")]
        public async Task<IActionResult> GetAllAdmissionsAsync()
        {
            try
            {
                var response = await _inpatientAdmissionManager.GetAllAdmissionsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActiveAdmissionsAsync")]
        public async Task<IActionResult> GetActiveAdmissionsAsync()
        {
            try
            {
                var response = await _inpatientAdmissionManager.GetActiveAdmissionsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAdmissionByIdAsync/{admissionId}")]
        public async Task<IActionResult> GetAdmissionByIdAsync(Guid admissionId)
        {
            try
            {
                var response = await _inpatientAdmissionManager.GetAdmissionByIdAsync(admissionId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateAdmissionAsync")]
        public async Task<IActionResult> InsertOrUpdateAdmissionAsync([FromBody] InpatientAdmission admission)
        {
            try
            {
                var response = await _inpatientAdmissionManager.InsertOrUpdateAdmissionAsync(admission);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteAdmissionAsync/{admissionId}")]
        public async Task<IActionResult> DeleteAdmissionAsync(Guid admissionId)
        {
            try
            {
                var result = await _inpatientAdmissionManager.DeleteAdmissionAsync(admissionId);
                if (result)
                {
                    return NoContent(); // 204 No Content
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAdmissionsByPatientIdAsync/{patientId}")]
        public async Task<IActionResult> GetAdmissionsByPatientIdAsync(Guid patientId)
        {
            try
            {
                var response = await _inpatientAdmissionManager.GetAdmissionsByPatientIdAsync(patientId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAdmissionsByHospitalIdAsync/{hospitalId}")]
        public async Task<IActionResult> GetAdmissionsByHospitalIdAsync(Guid hospitalId)
        {
            try
            {
                var response = await _inpatientAdmissionManager.GetAdmissionsByHospitalIdAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMostRecentAdmissionAsync/{patientId}")]
        public async Task<IActionResult> GetMostRecentAdmissionAsync(Guid patientId)
        {
            try
            {
                var response = await _inpatientAdmissionManager.GetMostRecentAdmissionAsync(patientId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Additional methods can be added here as needed
    }

}
