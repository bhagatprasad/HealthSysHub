using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [HealthSysHubAutherize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionManager _subscriptionManager;

        public SubscriptionController(ISubscriptionManager subscriptionManager)
        {
            _subscriptionManager = subscriptionManager;
        }

        [HttpGet]
        [Route("GetSubscriptionsAsync")]
        public async Task<IActionResult> GetSubscriptionsAsync()
        {
            try
            {
                var response = await _subscriptionManager.GetSubscriptionsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetHospitalSubscriptionsAsync/{hospitalId}")]
        public async Task<IActionResult> GetHospitalSubscriptionsAsync(Guid hospitalId)
        {
            try
            {
                var response = await _subscriptionManager.GetHospitalSubscriptionsAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLabSubscriptionsAsync/{labId}")]
        public async Task<IActionResult> GetLabSubscriptionsAsync(Guid labId)
        {
            try
            {
                var response = await _subscriptionManager.GetLabSubscriptionsAsync(labId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmacySubscriptionsAsync/{pharmacyId}")]
        public async Task<IActionResult> GetPharmacySubscriptionsAsync(Guid pharmacyId)
        {
            try
            {
                var response = await _subscriptionManager.GetPharmacySubscriptionsAsync(pharmacyId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateSubscriptionAsync")]
        public async Task<IActionResult> InsertOrUpdateSubscriptionAsync(Subscription subscription)
        {
            try
            {
                var response = await _subscriptionManager.InsertOrUpdateSubscriptionAsync(subscription);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
