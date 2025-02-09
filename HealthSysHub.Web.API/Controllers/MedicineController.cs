using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineManager _medicineManager;

        public MedicineController(IMedicineManager medicineManager)
        {
            _medicineManager = medicineManager;
        }

        [HttpGet]
        [Route("GetMedicinesAsync")]
        public async Task<IActionResult> GetMedicinesAsync()
        {
            try
            {
                var response = await _medicineManager.GetMedicinesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMedicineByIdAsync/{medicineId}")]
        public async Task<IActionResult> GetMedicineByIdAsync(Guid medicineId)
        {
            try
            {
                var response = await _medicineManager.GetMedicineByIdAsync(medicineId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [Route("InsertOrUpdateMedicineAsync")]
        public async Task<IActionResult> InsertOrUpdateMedicineAsync(Medicine medicine)
        {
            try
            {
                var response = await _medicineManager.InsertOrUpdateMedicineAsync(medicine);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
