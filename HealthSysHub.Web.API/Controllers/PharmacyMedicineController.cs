using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class PharmacyMedicineController : ControllerBase
    {
        private readonly IPharmacyMedicineManager _medicineManager;

        public PharmacyMedicineController(IPharmacyMedicineManager medicineManager)
        {
            _medicineManager = medicineManager;
        }

        [HttpGet]
        [Route("GetPharmacyMedicineAsync")]
        public async Task<IActionResult> GetPharmacyMedicineAsync()
        {
            try
            {
                var response = await _medicineManager.GetPharmacyMedicineAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPharmacyMedicineAsync/{pharmacyId}")]
        public async Task<IActionResult> GetPharmacyMedicineAsync(Guid pharmacyId)
        {
            try
            {
                var response = await _medicineManager.GetPharmacyMedicineAsync(pharmacyId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPharmacyMedicineByMedicineIdAsync/{medicineId}")]
        public async Task<IActionResult> GetPharmacyMedicineByMedicineIdAsync(Guid medicineId)
        {
            try
            {
                var response = await _medicineManager.GetPharmacyMedicineByMedicineIdAsync(medicineId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [Route("InsertOrUpdatePharmacyMedicineAsync")]
        public async Task<IActionResult> InsertOrUpdatePharmacyMedicineAsync(PharmacyMedicine medicine)
        {
            try
            {
                var response = await _medicineManager.InsertOrUpdatePharmacyMedicineAsync(medicine);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
