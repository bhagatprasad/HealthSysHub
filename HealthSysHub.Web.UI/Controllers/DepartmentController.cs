using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly INotyfService _notyfService;
        public DepartmentController(IDepartmentService departmentService,
            INotyfService notyfService)
        {
            _departmentService = departmentService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FetchDepartments()
        {
            try
            {
                var responce = await _departmentService.GetDepartmentsAsync();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateDepartment([FromBody] Department department)
        {
            try
            {
                var response = await _departmentService.InsertOrUpdateDepartmentAsync(department);
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
