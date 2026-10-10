using GoPress.Mvc.Areas.Admin.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly ApiService _apiService;
        public AdminOrdersController(ApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpGet]
        public async Task<IActionResult> AllOrderByAdmin()
        {
            var response=await _apiService.GetAsync<Response<List<OrdersResponseViewModel>>>
                (
                    "api/Admin/Orders/All-Orders"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load orders.";
                return View(new List<OrdersResponseViewModel>());
            }

                return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetailsByAdmin(int orderId)
        {
            var response = await _apiService.GetAsync<Response<OrdersResponseViewModel>>
              (
                  $"api/Admin/Orders/{orderId}/OrderDetails"
              );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load orders.";
                return View(new OrdersResponseViewModel());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GepAllPendingOrderByAdmin()
        {
            var response = await _apiService.GetAsync<Response<List<AdminOrderViewModel>>>
                (
                  "api/Admin/Orders/Pending-Orders"
                );

            if(response==null || response.Data == null)
            {
                TempData["Error"] = "Unable To load Pending Order";
                return View(new Response<List<AdminOrderViewModel>>());
            }
            return View(response);
        }

    }
}
