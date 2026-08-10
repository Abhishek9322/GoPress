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


        [HttpGet("{id}")]   
        public async Task<IActionResult>Details(int id)
        {
            var response =
                          await _apiService.GetAsync<
                           Response<OrderDetailsViewModel>>(
                          $"api/Customers/orders/{id}");

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load order details.";

                return RedirectToAction(
                    nameof(AllOrders));
            }

            return View(response.Data);

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


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderViewModel request)
        {
            try
            {
                var response =
                    await _apiService.PostAsync<
                        CreateOrderViewModel,
                        Response<int>>(
                            "api/Customers/orders",
                            request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        succeeded = false,
                        message = ex.Message
                    });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response =
                await _apiService.GetAsync<
                    Response<OrderViewModel>>(
                        $"api/Customers/orders/{id}");

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load order.";

                return RedirectToAction(nameof(AllOrders));
            }

            var order = response.Data;

            // Only pending orders can be edited
            if (order.Status.ToString() != "Pending")
            {
                TempData["Error"] =
                    "Only pending orders can be updated.";

                return RedirectToAction(nameof(Details),
                    new { id });
            }

            var model = new UpdateOrderViewModel
            {
                OrderId = order.Id,
                PickupAddress = order.PickupAddress,
                DeliveryAddress = order.DeliveryAddress,
                PickupDate = order.PickupDate,
                Notes = order.Notes,
                OrderItems = order.OrderItems
                    .Select(x => new UpdateOrderItemViewModel
                    {
                        ClothTypeId = x.ClothTypeId,
                     
                        Quantity = x.Quantity,
                   
                    }) .ToList()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var response =
                    await _apiService.PutAsync<
                        UpdateOrderViewModel,
                        Response<string>>(
                            $"api/Customers/orders/{model.OrderId}",
                            model);

                if (response == null)
                {
                    TempData["Error"] = "Unable to update order.";

                    return View(model);
                }

                if (!response.Succeeded)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        response.Message ?? "Unable to update order.");

                    return View(model);
                }

                TempData["Success"] =
                    response.Message ?? "Order updated successfully.";

                return RedirectToAction(
                    nameof(Details),
                    new { id = model.OrderId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                return View(model);
            }
        }
    }
}
