using GoPress.Mvc.Areas.Customer.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class DashboardController : Controller
    {
        private readonly ApiService _apiService;
        public DashboardController(ApiService apiService)
        {
            _apiService = apiService;   
        }
        public async Task<IActionResult> Dashboard()
        {
            var response = await _apiService.GetAsync<Response<CustomerDashboardViewModel>>
               (
                   "api/Customer/Dashboard"
               );
            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load dashboard.";

                return View(new CustomerDashboardViewModel());
            }


            return View(response.Data);
        }
    }
}
