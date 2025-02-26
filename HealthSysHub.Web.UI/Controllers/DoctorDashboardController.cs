using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    public class DoctorDashboardController : Controller
    {
        public DoctorDashboardController()
        {
                
        }
        public IActionResult ManageDoctorAppointments()
        {
            return View();
        }
    }
}
