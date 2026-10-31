using GoPress.Mvc.Areas.DeliveryBoy.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.DeliveryBoy.Controllers
{
    [Area("DeliveryBoy")]
    public class DeliveryBoyProfileController : Controller
    {
        private readonly ApiService _apiService;    
        public DeliveryBoyProfileController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeliveryBoyProfile()
        {
            var response = await _apiService.GetAsync<Response<DeliveryBoyProfileViewModel>>
                (
                "api/delivery-boys/Profiles/Get-Profile"
                );

            if(response==null || response.Data==null)
            {
                TempData["ErrorMessage"] = "Failed to retrieve profile data.";
                return View(new DeliveryBoyProfileViewModel());
            }
            return View(response.Data);
        }


        [HttpGet]
        public async Task<IActionResult> GetUpdatedDeliveryBoyProfile()
        {
            var response = await _apiService.GetAsync<Response<UpdateDeliveryProfileViewModel>>
                (
                   "api/delivery-boys/Profiles/Get-Profile"
                );

            if(response==null || response.Data==null)
            {
                TempData["Error"] = "Failed To Load Delivery Boy Profile .";
                return RedirectToAction(nameof(GetDeliveryBoyProfile));
            }

            var model = new UpdateDeliveryProfileViewModel
            {
                FullName=response.Data.FullName,
                PhoneNumber=response.Data.PhoneNumber,
                Address=response.Data.Address,
                City=response.Data.City,
                State=response.Data.State,
                Pincode=response.Data.Pincode,
                BikeNumber=response.Data.BikeNumber,
                LicenseNumber=response.Data.LicenseNumber,
                AadhaarNumber=response.Data.AadhaarNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDeliveryBoyProfile(UpdateDeliveryProfileViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("GetDeliveryBoyProfile",model);
            }

            var response=await _apiService.PutAsync<UpdateDeliveryProfileViewModel,Response<string>>
                (

                "api/delivery-boys/Profiles/Update-profile",model
                );

            if (response == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to update Delivery Boy profile.");
                return View(model);
            }

            TempData["Success"] = "Delivery Boy profile updated successfully.";
            return RedirectToAction(nameof(GetDeliveryBoyProfile));
        }
    }
}
