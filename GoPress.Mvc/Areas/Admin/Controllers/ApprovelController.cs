using GoPress.Mvc.Areas.Admin.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApprovelController : Controller
    {
        private readonly ApiService _apiService;    
        public ApprovelController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingShopOwners()
        {
            var response = await _apiService.GetAsync<Response<List<PendingShopOwnerViewModel>>>
                (
                   "api/AdminApprovel/pending-shopowners"
                );

            if(response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load pending shop owners.";
                return View(new List<PendingShopOwnerViewModel>());
            }
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingDeliveryBoy()
        {
            var response = await _apiService.GetAsync<Response<List<PendingDeliveryBoyViewModel>>>
                (
                   "api/AdminApprovel/pending-deliveryboys"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load pending shop owners.";
                return View(new List<PendingDeliveryBoyViewModel>());
            }
                return View(response.Data);
        }


    }
}
