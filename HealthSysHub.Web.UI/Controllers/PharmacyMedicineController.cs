using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class PharmacyMedicineController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IPharmacyMedicineService _pharmacyMedicineService;
        public PharmacyMedicineController(IPharmacyMedicineService pharmacyMedicineService, INotyfService notyfService)
        {
            _pharmacyMedicineService = pharmacyMedicineService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPharmacyMedicine()
        {
            try
            {
                var response = await _pharmacyMedicineService.GetPharmacyMedicineAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPharmacyMedicins(Guid pharmacyId)
        {
            try
            {
                var response = await _pharmacyMedicineService.GetPharmacyMedicineAsync(pharmacyId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPharmacyMedicineByMedicineId(Guid medicineId)
        {
            try
            {
                var response = await _pharmacyMedicineService.GetPharmacyMedicineByMedicineIdAsync(medicineId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePharmacyMedicine([FromBody] PharmacyMedicine pharmacyMedicine)
        {
            try
            {
                var response = await _pharmacyMedicineService.InsertOrUpdatePharmacyMedicineAsync(pharmacyMedicine);
                _notyfService.Success("Pharmacy Medicine details processed successful");
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
