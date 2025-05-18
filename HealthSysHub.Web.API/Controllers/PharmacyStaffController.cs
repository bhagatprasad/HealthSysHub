using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyStaffController : ControllerBase
    {
        private readonly IPharmacyStaffManager _pharmacyStaffManager;

        public PharmacyStaffController(IPharmacyStaffManager pharmacyStaffManager)
        {
            _pharmacyStaffManager = pharmacyStaffManager;
        }

        [HttpPost]
        [Route("InsertOrUpdatePharmacyStaffAsync")]
        public async Task<IActionResult> InsertOrUpdatePharmacyStaffAsync([FromBody] PharmacyStaff staff)
        {
            try
            {
                var response = await _pharmacyStaffManager.InsertOrUpdatePharmacyStaffAsync(staff);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmacyStaffByIdAsync/{staffId}")]
        public async Task<IActionResult> GetPharmacyStaffByIdAsync(Guid staffId)
        {
            try
            {
                var response = await _pharmacyStaffManager.GetPharmacyStaffByIdAsync(staffId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmacyStaffAsync")]
        public async Task<IActionResult> GetPharmacyStaffAsync([FromQuery] Guid? hospitalId, [FromQuery] Guid? pharmacyId)
        {
            try
            {
                var response = await _pharmacyStaffManager.GetPharmacyStaffAsync(hospitalId, pharmacyId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPharmacyStaffByharmacyAsync/{pharmacyId}")]
        public async Task<IActionResult> GetPharmacyStaffByharmacyAsync(Guid pharmacyId)
        {
            try
            {
                var response = await _pharmacyStaffManager.GetPharmacyStaffAsync(null, pharmacyId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmacyStaffsAsync")]
        public async Task<IActionResult> GetPharmacyStaffsAsync()
        {
            try
            {
                var response = await _pharmacyStaffManager.GetPharmacyStaffsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
