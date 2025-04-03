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
    public class HospitalTypeController : ControllerBase
    {
        private readonly IHospitalTypeManager _hospitalTypeManager;

        public HospitalTypeController(IHospitalTypeManager hospitalTypeManager)
        {
            _hospitalTypeManager = hospitalTypeManager;
        }

        [HttpGet]
        [Route("GetHospitalTypesAsync")]
        public async Task<IActionResult> GetHospitalTypesAsync()
        {
            try
            {
                var response = await _hospitalTypeManager.GetHospitalTypesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetHospitalTypeByIdAsync/{hospitalTypeId}")]
        public async Task<IActionResult> GetHospitalTypeByIdAsync(Guid hospitalTypeId)
        {
            try
            {
                var response = await _hospitalTypeManager.GetHospitalTypeAsync(hospitalTypeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateHospitalType")]
        public async Task<IActionResult> InsertOrUpdateHospitalType(HospitalType hospitalType)
        {
            try
            {
                var response = await _hospitalTypeManager.InsertOrUpdateHospitalTypeAsync(hospitalType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
