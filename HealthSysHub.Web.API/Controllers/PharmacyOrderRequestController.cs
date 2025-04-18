using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyOrderRequestController : ControllerBase
    {
        private readonly IPharmacyOrderRequestManager _pharmacyOrderRequestManager;

        public PharmacyOrderRequestController(IPharmacyOrderRequestManager pharmacyOrderRequestManager)
        {
            _pharmacyOrderRequestManager = pharmacyOrderRequestManager;
        }

        [HttpPost]
        [Route("InsertOrUpdatePharmacyOrderRequestAsync")]
        public async Task<IActionResult> InsertOrUpdatePharmacyOrderRequestAsync(PharmacyOrderRequestDetails requestDetails)
        {
            try
            {
                var response = await _pharmacyOrderRequestManager.InsertOrUpdatePharmacyOrderRequestDetailsAsync(requestDetails);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
