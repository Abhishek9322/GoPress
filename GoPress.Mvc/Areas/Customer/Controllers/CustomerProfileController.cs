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

            return View(response.Data);
        }
    }
}
