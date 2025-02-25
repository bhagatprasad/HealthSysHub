using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffManager _staffManager;

        public StaffController(IStaffManager staffManager)
        {
            _staffManager = staffManager;
        }

        [HttpGet]
        [Route("GetAllHospitalStaffAsync/{hospitalId}")]
        public async Task<IActionResult> GetAllHospitalStaffAsync(Guid hospitalId)
        {
            try
            {
                var response = await _staffManager.GetAllHospitalStaffAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetHospitalStaffAsync/{hospitalId}/{staffId}")]
        public async Task<IActionResult> GetHospitalStaffAsync(Guid hospitalId, Guid staffId)
        {
            try
            {
                var response = await _staffManager.GetHospitalStaffAsync(hospitalId, staffId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetHospitalDoctorsAsync/{hospitalId}")]
        public async Task<IActionResult> GetHospitalDoctorsAsync(Guid hospitalId)
        {
            try
            {
                var response = await _staffManager.GetDoctorsAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateHospitalStaffAsync")]
        public async Task<IActionResult> InsertOrUpdateHospitalStaffAsync(HospitalStaff hospitalStaff)
        {
            try
            {
                var response = await _staffManager.InsertOrUpdateHospitalStaffAsync(hospitalStaff);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
