using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    public class ReceptionistDashboardController : Controller
    {
        public ReceptionistDashboardController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
