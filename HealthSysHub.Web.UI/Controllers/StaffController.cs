using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        // Constructor with dependency injection
        private readonly IStaffService _staffService;
  
        private readonly INotyfService _notyfService;
        private readonly IPharmacyStaffService _pharmacyStaffService;
        public StaffController(IStaffService staffService,  INotyfService notyfService, IPharmacyStaffService pharmacyStaffService)
        {
            _staffService = staffService;
            _notyfService = notyfService;
            _pharmacyStaffService = pharmacyStaffService;
        }

        // Default action to return the Index view
        public IActionResult Index()
        {
            return View();
        }

        // HTTP GET method to fetch all hospital staff
        [HttpGet]
        public async Task<IActionResult> FetchAllHospitalStaff(Guid hospitalId)
        {
            try
            {
                var response = await _staffService.GetAllHospitalStaffAsync(hospitalId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        // HTTP GET method to fetch a specific hospital staff
        [HttpGet]
        public async Task<IActionResult> FetchHospitalStaff(Guid hospitalId, Guid staffId)
        {
            try
            {
                var response = await _staffService.GetHospitalStaffAsync(hospitalId, staffId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        // HTTP POST method to insert or update a hospital staff
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateHospitalStaff([FromBody] HospitalStaff hospitalStaff)
        {
            try
            {
                var response = await _staffService.InsertOrUpdateHospitalStaffAsync(hospitalStaff);
                _notyfService.Success("Staff details processed successful");
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPharmacyStaff(Guid? hospitalId, Guid? pharmacyId)
        {
            try
            {
                var response = await _pharmacyStaffService.GetPharmacyStaffAsync(hospitalId, pharmacyId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPharmacyStaffById(Guid staffId)
        {
            try
            {
                var response = await _pharmacyStaffService.GetPharmacyStaffByIdAsync(staffId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPharmacyStaffs(Guid staffId)
        {
            try
            {
                var response = await _pharmacyStaffService.GetPharmacyStaffsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

            [HttpPost]
            public async Task<IActionResult> InsertOrUpdatePharmacyStaff([FromBody] PharmacyStaff hospitalStaff)
            {
                try
                {
                    var response = await _pharmacyStaffService.InsertOrUpdatePharmacyStaffAsync(hospitalStaff);
                    _notyfService.Success("Staff details processed successful");
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
