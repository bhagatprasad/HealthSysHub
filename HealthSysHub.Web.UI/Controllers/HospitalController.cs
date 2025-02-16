using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.Utility.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{

    public class HospitalController : Controller
    {
        private readonly IHospitalService _hospitalService;
        private readonly INotyfService _notyfService;

        public HospitalController(IHospitalService hospitalService, INotyfService notyfService)
        {
            _hospitalService = hospitalService;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FetchHospitals()
        {
            try
            {
                var response = await _hospitalService.GetHospitalsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchHospitalById(Guid hospitalId)
        {
            try
            {
                var response = await _hospitalService.GetHospitalByIdAsync(hospitalId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> ManageHospital(Guid hospitalId)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateHospital([FromBody] Hospital hospital)
        {
            try
            {
                var response = await _hospitalService.InsertOrUpdateHospitalAsync(hospital);
                _notyfService.Success("Hospital details has been successfully saved.");
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchHospitalInformations()
        {
            try
            {
                var response = await _hospitalService.GetHospitalInformationsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchHospitalInformationById(Guid hospitalId)
        {
            try
            {
                var response = await _hospitalService.GetHospitalInformationByIdAsync(hospitalId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateHospitalContactInformation([FromBody] HospitalContactInformation hospitalContactInformation)
        {
            try
            {
                var response = await _hospitalService.InsertOrUpdateHospitalContactInformationAsync(hospitalContactInformation);
                _notyfService.Success("Hospital contact has been successfully saved.");
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateHospitalContentInformation([FromBody] HospitalContentInformation hospitalContentInformation)
        {
            try
            {
                var response = await _hospitalService.InsertOrUpdateHospitalContentInformationAsync(hospitalContentInformation);
                _notyfService.Success("Hospital content has been successfully saved.");
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateHospitalDepartmentInformation([FromBody] HospitalDepartmentInformation hospitalDepartmentInformation)
        {
            try
            {
                var response = await _hospitalService.InsertOrUpdateHospitalDepartmentInformationAsync(hospitalDepartmentInformation);
                _notyfService.Success("Hospital department has been successfully saved.");
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateHospitalSpecialtyInformation([FromBody] HospitalSpecialtyInformation hospitalSpecialtyInformation)
        {
            try
            {
                var response = await _hospitalService.InsertOrUpdateHospitalSpecialtyInformationAsync(hospitalSpecialtyInformation);
                _notyfService.Success("Hospital specialization has been successfully saved.");
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
