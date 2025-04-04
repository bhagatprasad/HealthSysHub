using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class HospitalTypeController : Controller
    {
        private readonly IHospitalTypeService _hospitalTypeService;
        private readonly INotyfService _notyfService;

        // Constructor with dependency injection
        public HospitalTypeController(IHospitalTypeService hospitalTypeService, INotyfService notyfService)
        {
            _hospitalTypeService = hospitalTypeService;
            _notyfService = notyfService;
        }

        // Default action to return the Index view
        public IActionResult Index()
        {
            return View();
        }

        // HTTP GET method to fetch hospital types
        [HttpGet]
        public async Task<IActionResult> FetchHospitalTypes()
        {
            try
            {
                var response = await _hospitalTypeService.GetHospitalTypesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        // HTTP POST method to insert or update a hospital type
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateHospitalType([FromBody] HospitalType hospitalType)
        {
            try
            {
                var response = await _hospitalTypeService.InsertOrUpdateHospitalTypeAsync(hospitalType);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
