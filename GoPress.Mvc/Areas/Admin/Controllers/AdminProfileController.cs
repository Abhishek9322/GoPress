using GoPress.Mvc.Areas.Admin.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProfileController : Controller
    {
        private readonly ApiService _apiService;
        public AdminProfileController(ApiService apiService)
        {
            _apiService = apiService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAdminProfile()
        {
            var response = await _apiService.GetAsync<Response<AdminProfileViewModel>>
                (
                 "api/Admin/Profiles/Get-Profile"
                );

            if(response == null || response.Data==null)
            {
                TempData["Error"] = "Unable to Load Admin profile";
                return View(new AdminProfileViewModel());
            }

            return View(response.Data);
        }
    }
}
