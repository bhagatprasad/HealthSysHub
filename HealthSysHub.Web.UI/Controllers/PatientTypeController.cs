using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    public class PatientTypeController : Controller
    {
        private readonly IPatientTypeService _patientTypeService;
        private readonly INotyfService _notyfService;

        // Constructor with dependency injection
        public PatientTypeController(IPatientTypeService patientTypeService, INotyfService notyfService)
        {
            _patientTypeService = patientTypeService;
            _notyfService = notyfService;
        }

        // Default action to return the Index view
        public IActionResult Index()
        {
            return View();
        }

        // HTTP GET method to fetch patient types
        [HttpGet]
        public async Task<IActionResult> FetchPatientTypes()
        {
            try
            {
                var response = await _patientTypeService.GetPatientTypesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        // HTTP POST method to insert or update a patient type
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePatientType([FromBody] PatientType patientType)
        {
            try
            {
                var response = await _patientTypeService.InsertOrUpdatePatientTypeAsync(patientType);
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
