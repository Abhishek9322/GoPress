using GoPress.Mvc.Areas.Admin.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {

        private readonly ApiService _apiService;
        public DashboardController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Dashboard()
        {
            var response = await _apiService.GetAsync<Response<AdminDashboardViewModel>>
                (
                   "api/Admin/Dashboard"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load dashboard.";
                return View(new AdminDashboardViewModel());
            }
                return View(response.Data);
        }
    }
}
