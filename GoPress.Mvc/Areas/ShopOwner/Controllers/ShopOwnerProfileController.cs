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

        [HttpGet]
        public async Task<IActionResult> GetUpdateShopOwnerProfile()
        {
            var response = await _apiService.GetAsync<Response<UpdateShopOwnerProfileViewModel>>
                (
                "api/ShopOwner/Profiles/Get-Profile"
                );

            if(response==null || response.Data==null)
            {
                TempData["Error"] = "Unable to load profile ";
                return RedirectToAction(nameof(GetShopOwnerProfile));
            }

            var model = new UpdateShopOwnerProfileViewModel
            {
                FullName=response.Data.FullName,
                PhoneNumber=response.Data.PhoneNumber,
                ShopName=response.Data.ShopName,
                ShopAddress=response.Data.ShopAddress,
                City=response.Data.City,
                State=response.Data.State,
                Pincode=response.Data.Pincode,
                ShopLicenseNumber=response.Data.ShopLicenseNumber,
                GSTNumber=response.Data.GSTNumber

            };

             return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateShopOwnerProfile(UpdateShopOwnerProfileViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("GetShopOwnerProfile",model);
            }

            var response = await _apiService.PutAsync<UpdateShopOwnerProfileViewModel,Response<string>>
                (
                    "api/ShopOwner/Profiles/Update-Profile",model
                );

            if(response==null)
            {
               ModelState.AddModelError(string.Empty,"Unable to update profile");
                return View(model);
            }

            return RedirectToAction(nameof(GetShopOwnerProfile));
        }
    }
}
