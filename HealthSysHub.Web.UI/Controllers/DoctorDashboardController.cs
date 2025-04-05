using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class DoctorDashboardController : Controller
    {
        public DoctorDashboardController()
        {
                
        }
        public IActionResult ManageDoctorAppointments()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreatePatientConsulation(Guid appointmentId)
        {
            return View();
        }
    }
}
