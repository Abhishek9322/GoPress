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
            var orders = await _apiService.GetAsync<Response<List<AllReadyFordeliveryOrders>>>
           (
                "api/delivery-boys/orders/ready-for-delivery"
           );

            if(orders == null || orders.Data == null)
            {
                TempData["Error"] = "Unable to load orders.";
                return View(new List<AllReadyFordeliveryOrders>());
            }

            return View(orders.Data);
        }
    }
}
