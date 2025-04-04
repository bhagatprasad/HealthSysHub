using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class MedicineController : Controller
    {
        private readonly IMedicineService _medicineService;
        private readonly INotyfService _notyfService;

        // Constructor with dependency injection
        public MedicineController(IMedicineService medicineService, INotyfService notyfService)
        {
            _medicineService = medicineService;
            _notyfService = notyfService;
        }

        // Default action to return the Index view
        public IActionResult Index()
        {
            return View();
        }

        // HTTP GET method to fetch medicines
        [HttpGet]
        public async Task<IActionResult> FetchMedicines()
        {
            try
            {
                var response = await _medicineService.GetMedicinesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        // HTTP POST method to insert or update a medicine
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateMedicine([FromBody] Medicine medicine)
        {
            try
            {
                var response = await _medicineService.InsertOrUpdateMedicineAsync(medicine);
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
