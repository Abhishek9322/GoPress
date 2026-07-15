using GoPress.Mvc.Areas.Customer.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrdersController : Controller
    {
        private readonly ApiService _apiService;

        public OrdersController(ApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            var response =
                await _apiService.GetAsync<
                    Response<List<OrderViewModel>>>(
                    "api/Customers/orders");

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load orders.";

                return View(new List<OrderViewModel>());
            }

            return View(response.Data);
        }
    }
}
