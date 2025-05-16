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


        [HttpGet]
        [Route("GetPharmacyOrderRequestsAsync")]
        public async Task<IActionResult> GetPharmacyOrderRequestsAsync()
        {
            try
            {
                var response = await _pharmacyOrderRequestManager.GetPharmacyOrderRequestsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPharmacyOrderRequestsByPharmacyAsync/{pharmacyId}")]
        public async Task<IActionResult> GetPharmacyOrderRequestsByPharmacyAsync(Guid pharmacyId)
        {
            try
            {
                var response = await _pharmacyOrderRequestManager.GetPharmacyOrderRequestsByPharmacyAsync(pharmacyId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPharmacyOrderRequestsByHospitalAsync/{hospitalId}")]
        public async Task<IActionResult> GetPharmacyOrderRequestsByHospitalAsync(Guid hospitalId)
        {
            try
            {
                var response = await _pharmacyOrderRequestManager.GetPharmacyOrderRequestsByHospitalAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPharmacyOrderRequestsByPatientAsync/{patientId}")]
        public async Task<IActionResult> GetPharmacyOrderRequestsByPatientAsync(Guid patientId)
        {
            try
            {
                var response = await _pharmacyOrderRequestManager.GetPharmacyOrderRequestsByPatientAsync(patientId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPharmacyOrderRequestDetailAsync/{pharmacyOrderRequestId}")]
        public async Task<IActionResult> GetPharmacyOrderRequestDetailAsync(Guid pharmacyOrderRequestId)
        {
            try
            {
                var response = await _pharmacyOrderRequestManager.GetPharmacyOrderRequestDetailAsync(pharmacyOrderRequestId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
