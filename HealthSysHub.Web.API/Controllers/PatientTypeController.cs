using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientTypeController : ControllerBase
    {
        private readonly IPatientTypeManager _patientTypeManager;

        public PatientTypeController(IPatientTypeManager patientTypeManager)
        {
            _patientTypeManager = patientTypeManager;
        }

        [HttpGet]
        [Route("GetPatientTypesAsync")]
        public async Task<IActionResult> GetPatientTypesAsync()
        {
            try
            {
                var response = await _patientTypeManager.GetPatientTypesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPatientTypeByIdAsync/{patientTypeId}")]
        public async Task<IActionResult> GetPatientTypeByIdAsync(Guid patientTypeId)
        {
            try
            {
                var response = await _patientTypeManager.GetPatientTypeByIdAsync(patientTypeId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdatePatientTypeAsync")]
        public async Task<IActionResult> InsertOrUpdatePatientTypeAsync(PatientType patientType)
        {

            try
            {
                var response = await _patientTypeManager.InsertOrUpdatePatientTypeAsync(patientType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
