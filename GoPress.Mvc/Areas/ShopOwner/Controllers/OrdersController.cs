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
        public async Task<IActionResult> AcceptedOrders()
        {
            var response = await _apiService.GetAsync<Response<List<AllAcceptedOrdersViewModel>>>
                (
                   "api/ShopOwner/Orders/Accepted-orders"
                );
            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load accepted orders.";
                return View(new List<AllAcceptedOrdersViewModel>());
            }
            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptOrders(int id)
        {
            try
            {
                var response =
                    await _apiService.PutAsync<Response<string>>(
                        $"api/ShopOwner/Orders/{id}/accept-order"
                    );

                if (response == null || !response.Succeeded)
                {
                    TempData["Error"] =
                        response?.Message ?? "Unable to accept this order.";

                    return RedirectToAction(nameof(AllOrders));
                }

                TempData["Success"] =
                    response.Message ?? "Order accepted successfully.";

                return RedirectToAction(nameof(AcceptedOrders));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(AllOrders));
            }
        }



        [HttpGet]
        public async Task<IActionResult> RejectedOrders()
        {
            var response = await _apiService.GetAsync<Response<List<AllRejectedOrderShopOwnerViewModel>>>
                (
                   "api/ShopOwner/Orders/all-rejected-orders"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load rejected orders.";
                return View(new List<AllRejectedOrderShopOwnerViewModel>());
            }

            return View(response.Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectOrder(int id)
        {
            try
            {
                var response =
                    await _apiService.PutAsync<Response<string>>(
                        $"api/ShopOwner/Orders/{id}/reject-order"
                    );

                if (response == null || !response.Succeeded)
                {
                    TempData["Error"] =
                        response?.Message ?? "Unable to reject this order.";

                    return RedirectToAction(nameof(AllOrders));
                }

                TempData["Success"] =
                    response.Message ?? "Order rejected successfully.";

                return RedirectToAction(nameof(RejectedOrders));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(AllOrders));
            }
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartProcessingOrders(int id)
        {
            try
            {
                var response = await _apiService.PutAsync<Response<string>>
               (
                 $"api/ShopOwner/Orders/{id}/start-processing"
               );
                if(response==null && !response.Succeeded)
                {
                    TempData["Error"] =response?.Message ?? "Unable to Start Processing this order.";

                    return RedirectToAction(nameof(GetCompletePickedUpOrders));
                }


                TempData["Success"] =
                   response.Message ?? "Order Started Processing successfully.";

                return RedirectToAction(nameof(GetProcessingOrders));


            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(GetCompletePickedUpOrders));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReadyForDevleryOrders(int id)
        {
            try
            {
                var response = await _apiService.PutAsync<Response<String>>
                    (
                      $"api/ShopOwner/Orders/{id}/ready-for-delivery"
                    );

                if(response==null && !response.Succeeded)
                {
                    TempData["Error"] = response?.Message ?? "Unable to Ready For Delivery Orders .";
                    return RedirectToAction(nameof(GetProcessingOrders));
                }
                return RedirectToAction(nameof(GetReadyForDelivery));

            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(GetProcessingOrders));
            }
          
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessingOrders()
        {
            var response = await _apiService.GetAsync<Response<List<AllStartProcessingOrdersViewModel>>>
                (
                 "api/ShopOwner/Orders/Start-Processing"
                );
            if(response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load processing orders.";
                return View(new List<AllStartProcessingOrdersViewModel>());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompletePickedUpOrders()
        {
            var response = await _apiService.GetAsync<Response<List<AllPickUpCompletedOrdersViewModel>>>
           (
             "api/ShopOwner/Orders/Pickup-Completed-Orders"
            );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load completed pickup orders.";
                return View(new List<AllPickUpCompletedOrdersViewModel>());
            }
            return View(response.Data);
        }

     /// <summary>
     /// 
     /// </summary>
     /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetReadyForDelivery()
        {
            var response = await _apiService.GetAsync<Response<List<ShopOwnerGetReadyForDelivery>>>
                (
                  "api/ShopOwner/Orders/All-ReadyforDelivery-Order"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable To Load GetReady For Delivery Orders";
                return View(new List<ShopOwnerGetReadyForDelivery>());
            }

            return View(response.Data);
        }




        [HttpGet]
        public async Task<IActionResult> CompletedOrder()
        {
            var response = await _apiService.GetAsync<Response<List<AllCompletedOrdersViewModel>>>
                (
                   "api/ShopOwner/Orders/completed-orders"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load completed orders.";
                return View(new List<AllCompletedOrdersViewModel>());
            }
            return View(response.Data);
        }







    }
}
