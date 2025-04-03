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
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeManager _paymentTypeManager;

        public PaymentTypeController(IPaymentTypeManager paymentTypeManager)
        {
            _paymentTypeManager = paymentTypeManager;
        }

        [HttpGet]
        [Route("GetPaymentTypesAsync")]
        public async Task<IActionResult> GetPaymentTypesAsync()
        {
            try
            {
                var response = await _paymentTypeManager.GetPaymentTypesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPaymentTypeByIdAsync/{paymentTypeId}")]
        public async Task<IActionResult> GetPaymentTypeByIdAsync(Guid paymentTypeId)
        {
            try
            {
                var response = await _paymentTypeManager.GetPaymentTypeByIdAsync(paymentTypeId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/paymenttype
        [HttpPost]
        [Route("InsertOrUpdatePaymentTypeAsync")]
        public async Task<IActionResult> InsertOrUpdatePaymentTypeAsync(PaymentType paymentType)
        {

            try
            {
                var response = await _paymentTypeManager.InsertOrUpdatePaymentTypeAsync(paymentType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
