using GoPress.Mvc.Areas.Customer.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using System.Text.Json;
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


        //---------------------------------------------------
        // STEP 1
        // Empty Create page
        //---------------------------------------------------

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateOrderViewModel();

            return View(model);
        }


        //---------------------------------------------------
        // STEP 2
        // Browse Shops
        //---------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Browse()
        {
            var response =
                await _apiService.GetAsync<
                    Response<List<AvailableShopViewModel>>>(
                    "api/CustomerShop");


            if (response == null || response.Data == null)
            {
                return View(new List<AvailableShopViewModel>());
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetNearbyShops()
        {
            var response =
                await _apiService.GetAsync<
                    Response<List<AvailableShopViewModel>>>(
                    "api/CustomerShop");

            if (response == null || response.Data == null)
            {
                return Json(new List<AvailableShopViewModel>());
            }

            return Json(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetPriceList(int shopOwnerId)
        {
            var response =
                await _apiService.GetAsync<
                    Response<List<ShopPriceViewModel>>>(
                    $"api/CustomerShop/shop-owner/{shopOwnerId}/price-list");

            if (response == null)
            {
                return BadRequest();
            }

            return Json(response);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateOrder([FromBody] CreateOrderViewModel request)
        //{
        //    try
        //    {
        //        var response =
        //            await _apiService.PostAsync<CreateOrderViewModel, Response<int>>(
        //                "api/Customers/orders",
        //                request);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }
        //}

        //[HttpPost]
        //public IActionResult CreateOrder([FromBody] CreateOrderViewModel request)
        //{
        //    return Json(request);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateOrder(
    [FromBody] CreateOrderViewModel request)
        {
            try
            {
                // STEP 1: Verify MVC received the request
                Console.WriteLine("========== MVC CREATE ORDER ==========");

                Console.WriteLine(
                    $"ShopOwnerId: {request.SelectedShopOwnerId}");

                Console.WriteLine(
                    $"PickupAddress: {request.PickupAddress}");

                Console.WriteLine(
                    $"DeliveryAddress: {request.DeliveryAddress}");

                Console.WriteLine(
                    $"OrderItems Count: {request.OrderItems?.Count}");

                foreach (var item in request.OrderItems)
                {
                    Console.WriteLine(
                        $"ClothTypeId: {item.ClothTypeId}, Quantity: {item.Quantity}");
                }

                // STEP 2: Send request to API
                var response =
                    await _apiService.PostAsync<
                        CreateOrderViewModel,
                        Response<int>>(
                            "api/Customers/orders",
                            request);

                Console.WriteLine("========== API RESPONSE ==========");

                Console.WriteLine(
                    $"Succeeded: {response?.Succeeded}");

                Console.WriteLine(
                    $"Message: {response?.Message}");

                Console.WriteLine(
                    $"Data: {response?.Data}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("========== CREATE ORDER ERROR ==========");
                Console.WriteLine(ex.ToString());

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        succeeded = false,
                        message = ex.Message
                    });
            }
        }
    }
}
