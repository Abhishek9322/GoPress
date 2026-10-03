using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserManegementController(ApiService apiService,IMapper mapper)
        {
            _apiService= apiService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomerByAdmin()
        {
            var response =await _apiService.GetAsync<Response<List<AllCustomerProfileApiModel>>>
                (
                    "api/Admin/UserManagment/ALL-Customer"
                );

            if (response == null || response.Data == null)
            {
                TempData["Error"] = "Unable To Load All Customer.";

                return View(new List<AllCutomerProfileViewModel>());
            }
            var model =_mapper.Map<List<AllCutomerProfileViewModel>>(response.Data);

            return View(model);
        }
    }
}
