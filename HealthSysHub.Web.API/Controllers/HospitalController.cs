using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
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
        [Route("GetHospitalInformationsAsync")]
        public async Task<IActionResult> GetHospitalInformationsAsync()
        {
            try
            {
                var response = await _hospitalManager.GetHospitalInformationsAsync();
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
        [HttpGet]
        [Route("GetHospitalInformationByIdAsync/{hospitalId}")]
        public async Task<IActionResult> GetHospitalInformationByIdAsync(Guid hospitalId)
        {
            try
            {
                var response = await _hospitalManager.GetHospitalInformationByIdAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateHospitalAsync")]
        public async Task<IActionResult> InsertOrUpdateHospitalAsync(Hospital hospital)
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
        [HttpPost]
        [Route("InsertOrUpdateHospitalContactInformationAsync")]
        public async Task<IActionResult> InsertOrUpdateHospitalContactInformationAsync(HospitalContactInformation hospitalContactInformation)
        {
            try
            {
                var response = await _hospitalManager.InsertOrUpdateHospitalContactInformationAsync(hospitalContactInformation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateHospitalContentInformationAsync")]
        public async Task<IActionResult> InsertOrUpdateHospitalContentInformationAsync(HospitalContentInformation hospitalContentInformation)
        {
            try
            {
                var response = await _hospitalManager.InsertOrUpdateHospitalContentInformationAsync(hospitalContentInformation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateHospitalDepartmentInformationAsync")]
        public async Task<IActionResult> InsertOrUpdateHospitalDepartmentInformationAsync(HospitalDepartmentInformation hospitalDepartmentInformation)
        {
            try
            {
                var response = await _hospitalManager.InsertOrUpdateHospitalDepartmentInformationAsync(hospitalDepartmentInformation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("InsertOrUpdateHospitalSpecialtyInformationAsync")]
        public async Task<IActionResult> InsertOrUpdateHospitalSpecialtyInformationAsync(HospitalSpecialtyInformation hospitalSpecialtyInformation)
        {
            try
            {
                var response = await _hospitalManager.InsertOrUpdateHospitalSpecialtyInformationAsync(hospitalSpecialtyInformation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
