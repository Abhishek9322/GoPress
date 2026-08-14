using GoPress.Mvc.Areas.DeliveryBoy.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.DeliveryBoy.Controllers
{
    [Area("DeliveryBoy")]
    public class DashboardController : Controller
    {
        private readonly ApiService _apiService;
        public DashboardController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Dashboard()
        {
            var response = await _apiService.GetAsync<Response<DeliveryBoyDashboardViewModel>>
                 (
                     "api/delivery-boys/Dashboard"
                 );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load dashboard.";
                return View(new DeliveryBoyDashboardViewModel());
            }
            return View(response.Data);


        }
    }
}
