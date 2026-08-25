using GoPress.Mvc.Areas.ShopOwner.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.ShopOwner.Controllers
{
    [Area("ShopOwner")]

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
            var response = await _apiService.GetAsync<Response<List<AllOrderViewModel>>>
                (
                   "api/ShopOwner/Orders/all-orders"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load orders.";
                return View(new List<AllOrderViewModel>());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> RejectedOrders()
        {
            var response = await _apiService.GetAsync<Response<List<AllRejectedOrderShopOwner>>>
                (
                   "api/ShopOwner/Orders/all-rejected-orders"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load rejected orders.";
                return View(new List<AllRejectedOrderShopOwner>()); 
            }

            return View(response.Data);
        }
    }
}
