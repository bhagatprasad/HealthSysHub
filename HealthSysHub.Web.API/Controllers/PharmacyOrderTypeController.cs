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
    public class PharmacyOrderTypeController : ControllerBase
    {
        private readonly IPharmacyOrderTypeManager _pharmacyOrderTypeManager;

        public PharmacyOrderTypeController(IPharmacyOrderTypeManager pharmacyOrderTypeManager)
        {
            _pharmacyOrderTypeManager = pharmacyOrderTypeManager;
        }

        [HttpGet]
        [Route("GetPharmacyOrderTypesAsync")]
        public async Task<IActionResult> GetPharmacyOrderTypesAsync()
        {
            try
            {
                var response = await _pharmacyOrderTypeManager.GetPharmacyOrderTypesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmacyOrderTypeByIdAsync/{pharmacyOrderTypeId}")]
        public async Task<IActionResult> GetPharmacyOrderTypeByIdAsync(Guid pharmacyOrderTypeId)
        {
            try
            {
                var response = await _pharmacyOrderTypeManager.GetPharmacyOrderTypeByIdAsync(pharmacyOrderTypeId);

                if (response == null)
                {
                    return NotFound($"PharmacyOrderType with ID {pharmacyOrderTypeId} not found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdatePharmacyOrderTypeAsync")]
        public async Task<IActionResult> InsertOrUpdatePharmacyOrderTypeAsync(PharmacyOrderType pharmacyOrderType)
        {
            try
            {
                var response = await _pharmacyOrderTypeManager.InsertOrUpdatePharmacyOrderTypeAsync(pharmacyOrderType);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
