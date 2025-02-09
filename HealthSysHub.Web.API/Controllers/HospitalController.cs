using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalManager _hospitalManager;

        public HospitalController(IHospitalManager hospitalManager)
        {
            _hospitalManager = hospitalManager;
        }

        [HttpGet]
        [Route("GetHospitalsAsync")]
        public async Task<IActionResult> GetHospitalsAsync()
        {
            try
            {
                var response = await _hospitalManager.GetHospitalsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetHospitalByIdAsync/{hospitalId}")]
        public async Task<IActionResult> GetHospitalByIdAsync(Guid hospitalId)
        {
            try
            {
                var response = await _hospitalManager.GetHospitalByIdAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateHospital")]
        public async Task<IActionResult> InsertOrUpdateHospital(Hospital hospital)
        {
            try
            {
                var response = await _hospitalManager.InsertOrUpdateHospitalAsync(hospital);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
