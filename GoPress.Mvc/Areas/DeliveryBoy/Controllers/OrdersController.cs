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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptPickup(int id)
        {
            try
            {
                var response = await _apiService.PutAsync<Response<string>>(
                    $"api/delivery-boys/orders/{id}/accept"
                    );

                if(response==null || !response.Succeeded)
                {
                    TempData["Error"] = "Unable To Accept Pickup order.";
                    return RedirectToAction(nameof(AvailableOrders));
                }
                TempData["Success"] = response.Message ?? "Orders accepted Successfully.";
                return RedirectToAction(nameof(GetAllAcceptPickUpOrders));

            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(AvailableOrders));
            }

            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAcceptPickUpOrders()
        {
            var orders = await _apiService.GetAsync<Response<List<GetAllAcceptPickUpOrdersViewModel>>>
                (
                  "api/delivery-boys/orders/PickUpOrdersDBoy"
                );

            if(orders==null || orders.Data==null)
            {
                TempData["Error"] = "Unable To Load Orders.";
                return View(new List<GetAllAcceptPickUpOrdersViewModel>());
            }
            return View(orders.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptPickUpCompeleted(int id)
        {
            try
            {
                var response = await _apiService.PutAsync<Response<string>>(
                    $"api/delivery-boys/orders/{id}/pickup-completed"
                    );

                if(response==null || !response.Succeeded)
                {
                    TempData["Error"] = "Uable To make Pickup Comleted For This Order.";
                    return RedirectToAction(nameof(GetAllAcceptPickUpOrders));

                }

                TempData["Success"] = response.Message ?? "Pickup Comleted Successfully.";
                return RedirectToAction(nameof(AllReadyFordeliveryOrders));
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(AllReadyFordeliveryOrders));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAcceptPickUpCompeletedOrders()
        {
            var response = await _apiService.GetAsync<Response<List<AllPickUpCompletedOrdersByDeliveryBoyViewModel>>>
                (
                  "api/delivery-boys/orders/PickUpCompleteOrder"
                );

            if( response==null ||response.Data==null)
            {
                TempData["Error"] = "Get Pickup Comleted Successfully .";

                return View(new List<AllPickUpCompletedOrdersByDeliveryBoyViewModel>());
            }

            return View(response.Data);
        }



    }
}
