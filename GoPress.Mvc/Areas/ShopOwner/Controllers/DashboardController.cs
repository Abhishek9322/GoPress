using GoPress.Mvc.Areas.ShopOwner.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.ShopOwner.Controllers
{
    [Area("ShopOwner")]
    public class DashboardController : Controller
    {
        private readonly ApiService _apiService;
        public DashboardController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async  Task<IActionResult> Dashboard()
        {
            var response = await _apiService.GetAsync<Response<ShopOwnerDashboardViewModel>>
                (
                   "api/ShopOwner/Dashboard"
                );

            if(response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load dashboard.";
                return View(new ShopOwnerDashboardViewModel());
            }

                return View(response.Data);
        }
    }
}
