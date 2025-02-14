using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationManager _specializationManager;

        public SpecializationController(ISpecializationManager specializationManager)
        {
            _specializationManager = specializationManager;
        }

        [HttpGet]
        [Route("GetSpecializationByIdAsync/{specializationId}")]
        public async Task<ActionResult> GetSpecializationByIdAsync(Guid specializationId)
        {
            try
            {
                var specialization = await _specializationManager.GetSpecializationByIdAsync(specializationId);

                return Ok(specialization);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSpecializationsAsync")]
        public async Task<ActionResult> GetSpecializationsAsync()
        {
            try
            {
                var specializations = await _specializationManager.GetSpecializationsAsync();
                return Ok(specializations);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateSpecializationAsync")]
        public async Task<ActionResult> InsertOrUpdateSpecializationAsync(Specialization specialization)
        {
            try
            {
                var response = await _specializationManager.InsertOrUpdateSpecializationAsync(specialization);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
