using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorManager _doctorManager;
        public DoctorController(IDoctorManager doctorManager)
        {
            _doctorManager = doctorManager;
        }
        [HttpPost]
        [Route("InsertOrUpdateDoctorAsync")]
        public async Task<IActionResult> InsertOrUpdateDoctorAsync(Doctor doctor)
        {
            try
            {
                var result = await _doctorManager.InsertOrUpdateDoctorAsync(doctor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDoctorByIdAsync/{doctorId}")]
        public async Task<IActionResult> GetDoctorByIdAsync(Guid doctorId)
        {
            try
            {
                var doctor = await _doctorManager.GetDoctorByIdAsync(doctorId);
                return Ok(doctor);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDoctorsByHospitalAsync/{hospitalId}")]
        public async Task<IActionResult> GetDoctorsByHospitalAsync(Guid hospitalId)
        {
            try
            {
                var doctors = await _doctorManager.GetDoctorsAsync(hospitalId);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
