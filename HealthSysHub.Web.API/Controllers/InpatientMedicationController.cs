using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class InPatientMedicationController : ControllerBase
    {
        private readonly IInpatientMedicationManager _inpatientMedicationManager;

        public InPatientMedicationController(IInpatientMedicationManager inpatientMedicationManager)
        {
            _inpatientMedicationManager = inpatientMedicationManager;
        }

        [HttpGet]
        [Route("GetAllMedicationsAsync")]
        public async Task<IActionResult> GetAllMedicationsAsync()
        {
            try
            {
                var response = await _inpatientMedicationManager.GetAllMedicationsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActiveMedicationsAsync")]
        public async Task<IActionResult> GetActiveMedicationsAsync()
        {
            try
            {
                var response = await _inpatientMedicationManager.GetActiveMedicationsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMedicationByIdAsync/{medicationId}")]
        public async Task<IActionResult> GetMedicationByIdAsync(Guid medicationId)
        {
            try
            {
                var response = await _inpatientMedicationManager.GetMedicationByIdAsync(medicationId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateMedicationAsync")]
        public async Task<IActionResult> InsertOrUpdateMedicationAsync([FromBody] InpatientMedication medication)
        {
            try
            {
                var response = await _inpatientMedicationManager.InsertOrUpdateMedicationAsync(medication);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteMedicationAsync/{medicationId}")]
        public async Task<IActionResult> DeleteMedicationAsync(Guid medicationId)
        {
            try
            {
                var result = await _inpatientMedicationManager.DeleteMedicationAsync(medicationId);
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
        [Route("GetMedicationsByInpatientIdAsync/{inpatientId}")]
        public async Task<IActionResult> GetMedicationsByInpatientIdAsync(Guid inpatientId)
        {
            try
            {
                var response = await _inpatientMedicationManager.GetMedicationsByInpatientIdAsync(inpatientId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMedicationsByDoctorIdAsync/{doctorId}")]
        public async Task<IActionResult> GetMedicationsByDoctorIdAsync(Guid doctorId)
        {
            try
            {
                var response = await _inpatientMedicationManager.GetMedicationsByDoctorIdAsync(doctorId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMostRecentMedicationAsync/{inpatientId}")]
        public async Task<IActionResult> GetMostRecentMedicationAsync(Guid inpatientId)
        {
            try
            {
                var response = await _inpatientMedicationManager.GetMostRecentMedicationAsync(inpatientId);
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
