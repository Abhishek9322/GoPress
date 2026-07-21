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
    }
}
