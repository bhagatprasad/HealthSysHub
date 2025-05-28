using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.Managers;
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
    }
}
