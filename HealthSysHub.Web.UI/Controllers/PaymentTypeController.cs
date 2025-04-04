using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly INotyfService _notyfService;

        // Constructor with dependency injection
        public PaymentTypeController(IPaymentTypeService paymentTypeService, INotyfService notyfService)
        {
            _paymentTypeService = paymentTypeService;
            _notyfService = notyfService;
        }

        // Default action to return the Index view
        public IActionResult Index()
        {
            return View();
        }

        // HTTP GET method to fetch payment types
        [HttpGet]
        public async Task<IActionResult> FetchPaymentTypes()
        {
            try
            {
                var response = await _paymentTypeService.GetPaymentTypesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        // HTTP POST method to insert or update a payment type
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePaymentType([FromBody] PaymentType paymentType)
        {
            try
            {
                var response = await _paymentTypeService.InsertOrUpdatePaymentTypeAsync(paymentType);
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
