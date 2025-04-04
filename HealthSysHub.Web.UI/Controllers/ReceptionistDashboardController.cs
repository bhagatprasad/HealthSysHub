using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
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
