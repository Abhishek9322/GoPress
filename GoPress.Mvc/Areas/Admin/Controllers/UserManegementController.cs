using GoPress.Mvc.Areas.Admin.Models;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoPress.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManegementController : Controller
    {
        private readonly ApiService _apiService;
        public UserManegementController(ApiService apiService)
        {
            _apiService= apiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomerByAdmin()
        {
            var response = await _apiService.GetAsync<Response<List<AllCutomerProfileViewModel>>>
               (
                   "api/Admin/UserManagment/ALL-Customer"
                );

            if(response==null ||response.Data==null)
            {
                TempData["Error"] = "Unable To Load All Customer .";
                return View(new List<AllCutomerProfileViewModel>());
            }

            return View(response.Data);
        }
    }
}
