using GoPress.Mvc.Areas.DeliveryBoy.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.DeliveryBoy.Controllers
{
    [Area("DeliveryBoy")]
    public class OrdersController : Controller
    {
        private readonly ApiService _apiService;
        public OrdersController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> AllReadyFordeliveryOrders()
        {
            var orders = await _apiService.GetAsync<Response<List<AllReadyFordeliveryOrdersViewModel>>>
           (
                "api/delivery-boys/orders/ready-for-delivery"
           );

            if(orders == null || orders.Data == null)
            {
                TempData["Error"] = "Unable to load orders.";
                return View(new List<AllReadyFordeliveryOrdersViewModel>());
            }

            return View(orders.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AvailableOrders()
        {
            var orders = await _apiService.GetAsync<Response<List<AvailableOrderViewModel>>>
           (
                "api/delivery-boys/orders/available"
           );
            if(orders==null || orders.Data == null)
            {
                TempData["Error"] = "Unable To Load Orders.";
                return View(new List<AvailableOrderViewModel>());
            }


            return View(orders.Data);
        }



    }
}
