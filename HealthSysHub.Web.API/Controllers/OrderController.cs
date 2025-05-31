using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class OrderController : ControllerBase
    {
        private readonly IPharmacyOrderManager _manager;

        public OrderController(IPharmacyOrderManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetPharmacyOrdersListByPharmacyAsync/{pharmacyId}")]
        public async Task<IActionResult> GetPharmacyOrdersListByPharmacyAsync(Guid pharmacyId)
        {
            try
            {
                var response = await _manager.GetPharmacyOrdersListByPharmacyAsync(pharmacyId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPharmacyOrderByIdAsync/{pharmacyId}/{pharmacyOrderId}")]
        public async Task<IActionResult> GetPharmacyOrderByIdAsync(Guid pharmacyId,Guid pharmacyOrderId)
        {
            try
            {
                var response = await _manager.GetPharmacyOrderByIdAsync(pharmacyId, pharmacyOrderId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("ProcessPharmacyOrdersRequestAsync")]
        public async Task<IActionResult> ProcessPharmacyOrdersRequestAsync(PharmacyOrdersProcessRequest request)
        {
            try
            {
                var response = await _manager.ProcessPharmacyOrdersRequestAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
