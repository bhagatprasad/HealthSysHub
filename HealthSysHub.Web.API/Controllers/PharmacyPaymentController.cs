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
    public class PharmacyPaymentController : ControllerBase
    {
        private readonly IPharmacyPaymentManager _pharmacyPaymentManager;
        public PharmacyPaymentController(IPharmacyPaymentManager pharmacyPaymentManager)
        {
            _pharmacyPaymentManager = pharmacyPaymentManager;
        }

        [HttpPost]
        [Route("ProcessPharmacyOrderPaymentAsync")]
        public async Task<IActionResult> ProcessPharmacyOrderPaymentAsync(PharmacyPayment pharmacyPayment)
        {
            try
            {
                var response = await _pharmacyPaymentManager.ProcessPharmacyOrderPaymentAsync(pharmacyPayment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmacyPaymentListAsync/{pharmacyId}")]
        public async Task<IActionResult> GetPharmacyPaymentListAsync(Guid pharmacyId)
        {
            try
            {
                var currentDate= DateTimeOffset.UtcNow;

                var response = await _pharmacyPaymentManager.GetPharmacyPaymentListAsync(pharmacyId, currentDate);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmacyPaymentDetailAsync/{pharmacyId}/{pharmacyOrderId}")]
        public async Task<IActionResult> GetPharmacyPaymentDetailAsync(Guid pharmacyId,Guid pharmacyOrderId)
        {
            try
            {
                var currentDate = DateTimeOffset.UtcNow;

                var response = await _pharmacyPaymentManager.GetPharmacyPaymentDetailAsync(pharmacyId, pharmacyOrderId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
