using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class LabTestController : Controller
    {
        private readonly ILabTestService _labTestService;
        private readonly INotyfService _notyfService;

        // Constructor with dependency injection
        public LabTestController(ILabTestService labTestService, INotyfService notyfService)
        {
            _labTestService = labTestService;
            _notyfService = notyfService;
        }

        // Default action to return the Index view
        public IActionResult Index()
        {
            return View();
        }

        // HTTP GET method to fetch lab tests
        [HttpGet]
        public async Task<IActionResult> FetchLabTests()
        {
            try
            {
                var response = await _labTestService.GetLabTestsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        // HTTP POST method to insert or update a lab test
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateLabTest([FromBody] LabTest labTest)
        {
            try
            {
                var response = await _labTestService.InsertOrUpdateLabTestAsync(labTest);
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
