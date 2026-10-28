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
    }
}
