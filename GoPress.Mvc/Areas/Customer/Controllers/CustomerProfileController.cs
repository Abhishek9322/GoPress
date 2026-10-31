using GoPress.Mvc.Areas.Customer.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerProfileController : Controller
    {
        private readonly ApiService _apiService;
        public CustomerProfileController(ApiService apiService)
        {
            _apiService = apiService;   
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerProfile()
        {
            var response = await _apiService.GetAsync<Response<CustomerProfileViewModel>>
                (
                "api/Customer/Profiles/Get-Profile"
                );

            if(response==null || response.Data==null)
            {
                TempData["Error"] = "Unable to load Customer profile.";

                return View(new CustomerProfileViewModel());
            }
            TempData["Success"] = "Customer profile updated successfully.";
            return View(response.Data);
        }


        [HttpGet]
        public async Task<IActionResult> GetUpdateCustomerProfile()
        {
            var response = await _apiService.GetAsync<Response<CustomerProfileViewModel>>(
                "api/Customer/Profiles/Get-Profile"
            );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable to load Customer profile.";

                return RedirectToAction(nameof(GetCustomerProfile));
            }

            var model = new UpdateCustomerProfileViewModel
            {
                FullName = response.Data.FullName,
                PhoneNumber = response.Data.PhoneNumber
            };

           TempData["Success"] = "Customer profile loaded successfully.";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomerProfile(UpdateCustomerProfileViewModel model)
        {
            if(!ModelState.IsValid)
            {
               return View ("GetCustomerprofile", model);
            }

            var response = await _apiService.PutAsync<UpdateCustomerProfileViewModel,Response<string>>
                (
                "api/Customer/Profiles/Update-Profile",model
                );

            if(response==null )
            {
               ModelState.AddModelError(string.Empty, "Unable to update Customer profile.");
                return View(model);
            }

            TempData["Success"] ="Customer profile updated successfully.";
            return RedirectToAction(nameof(GetCustomerProfile));
        }
    }
}
