using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentManager _departmentManager;

        public DepartmentController(IDepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }

        [HttpGet]
        [Route("GetDepartmentsAsync")]
        public async Task<IActionResult> GetDepartmentsAsync()
        {
            try
            {
                var response = await _departmentManager.GetDepartmentsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDepartmentByIdAsync")]
        public async Task<IActionResult> GetDepartmentByIdAsync(Guid departmentId)
        {
            try
            {
                var response = await _departmentManager.GetDepartmentByIdAsync(departmentId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateDepartment")]
        public async Task<IActionResult> InsertOrUpdateDepartment(Department department)
        {
            try
            {
                var response = await _departmentManager.InsertOrUpdateDepartmentAsync(department);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
