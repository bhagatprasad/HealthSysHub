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
    public class InPatientTreatmentPlanController : ControllerBase
    {
        private readonly IInpatientTreatmentPlanManager _inpatientTreatmentPlanManager;

        public InPatientTreatmentPlanController(IInpatientTreatmentPlanManager inpatientTreatmentPlanManager)
        {
            _inpatientTreatmentPlanManager = inpatientTreatmentPlanManager;
        }

        [HttpGet]
        [Route("GetAllTreatmentPlansAsync")]
        public async Task<IActionResult> GetAllTreatmentPlansAsync()
        {
            try
            {
                var response = await _inpatientTreatmentPlanManager.GetAllTreatmentPlansAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActiveTreatmentPlansAsync")]
        public async Task<IActionResult> GetActiveTreatmentPlansAsync()
        {
            try
            {
                var response = await _inpatientTreatmentPlanManager.GetActiveTreatmentPlansAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTreatmentPlanByIdAsync/{treatmentPlanId}")]
        public async Task<IActionResult> GetTreatmentPlanByIdAsync(Guid treatmentPlanId)
        {
            try
            {
                var response = await _inpatientTreatmentPlanManager.GetTreatmentPlanByIdAsync(treatmentPlanId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateTreatmentPlanAsync")]
        public async Task<IActionResult> InsertOrUpdateTreatmentPlanAsync([FromBody] InpatientTreatmentPlan treatmentPlan)
        {
            try
            {
                var response = await _inpatientTreatmentPlanManager.InsertOrUpdateTreatmentPlanAsync(treatmentPlan);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteTreatmentPlanAsync/{treatmentPlanId}")]
        public async Task<IActionResult> DeleteTreatmentPlanAsync(Guid treatmentPlanId)
        {
            try
            {
                var result = await _inpatientTreatmentPlanManager.DeleteTreatmentPlanAsync(treatmentPlanId);
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
        [Route("GetTreatmentPlansByInpatientIdAsync/{inpatientId}")]
        public async Task<IActionResult> GetTreatmentPlansByInpatientIdAsync(Guid inpatientId)
        {
            try
            {
                var response = await _inpatientTreatmentPlanManager.GetTreatmentPlansByInpatientIdAsync(inpatientId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTreatmentPlansByDoctorIdAsync/{doctorId}")]
        public async Task<IActionResult> GetTreatmentPlansByDoctorIdAsync(Guid doctorId)
        {
            try
            {
                var response = await _inpatientTreatmentPlanManager.GetTreatmentPlansByDoctorIdAsync(doctorId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTreatmentPlansByStatusAsync/{status}")]
        public async Task<IActionResult> GetTreatmentPlansByStatusAsync(string status)
        {
            try
            {
                var response = await _inpatientTreatmentPlanManager.GetTreatmentPlansByStatusAsync(status);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
