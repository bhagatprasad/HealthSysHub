using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyManager _pharmacyManager;

        public PharmacyController(IPharmacyManager pharmacyManager)
        {
            _pharmacyManager = pharmacyManager;
        }

        [HttpGet]
        [Route("GetPharmaciesAsync")]
        public async Task<IActionResult> GetPharmaciesAsync()
        {
            try
            {
                var response = await _pharmacyManager.GetPharmaciesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmacyByIdAsync/{pharmacyId}")]
        public async Task<IActionResult> GetPharmacyByIdAsync(Guid pharmacyId)
        {
            try
            {
                var response = await _pharmacyManager.GetPharmacyByIdAsync(pharmacyId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdatePharmacyAsync")]
        public async Task<IActionResult> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy)
        {
            try
            {
                var response = await _pharmacyManager.InsertOrUpdatePharmacyAsync(pharmacy);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmaciesAsync/{searchString}")]
        public async Task<IActionResult> GetPharmaciesAsync(string searchString)
        {
            try
            {
                var response = await _pharmacyManager.GetPharmaciesAsync(searchString);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
