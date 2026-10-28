using GoPress.Mvc.Areas.ShopOwner.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.ShopOwner.Controllers
{
    [Area("ShopOwner")]
    public class ShopOwnerProfileController : Controller
    {
        private readonly ApiService _apiService;
        public ShopOwnerProfileController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShopOwnerProfile()
        {
            var response = await _apiService.GetAsync<Response<ShopOwnerProfileViewModel>>
                (
                  "api/ShopOwner/Profiles/Get-Profile"
                );

            if(response==null || response.Data==null)
            {
                TempData["Error"] = "Unable to load profile ";
                return View(new Response<ShopOwnerProfileViewModel>());
            }

            return View(response.Data);
        }
    }
}
