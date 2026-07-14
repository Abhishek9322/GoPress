using GoPress.Mvc.Areas.Auth.Auth;
using GoPress.Mvc.Helpers;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace GoPress.Mvc.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;
        private readonly ITokenService _tokenService;
        public AuthController(ApiService apiService, ITokenService tokenService )
        {
            _apiService = apiService;
            _tokenService = tokenService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel login)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var response =
                await _apiService.PostAsync<
                    LoginViewModel,
                    AuthResponseViewModel>(
                    "api/auth/All-Login",
                    login);

            if (!response.Success)
            {
                ViewBag.Error = response.Message;
                return View(login);
            }

            if (string.IsNullOrWhiteSpace(response.AccessToken))
            {
                ViewBag.Error = "Access token was not returned from the API.";
                return View(login);
            }

            _tokenService.SaveToken(response.AccessToken);

            var redirect =
                RoleRedirectHelper.GetRedirect(response.Role);

            return RedirectToAction(
                redirect.Action,
                redirect.Controller,
                new
                {
                    area = redirect.Area
                });

        }

        //Logout
        public IActionResult Logout()
        {
            _tokenService.RemoveToken();

            return RedirectToAction(
                "Login",
                "Auth",
                new { area = "Auth" });
        }

        // CUSTOMER REGISTER PAGE
        [HttpGet]
        public IActionResult RegisterCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerViewModel register)
        {
            var response =
               await _apiService
               .PostAsync<
                   RegisterCustomerViewModel,
                   AuthResponseViewModel>(
                   "api/Auth/register-customer",
                   register);

            if (!response.Success)
            {
                ViewBag.Error =
                    response.Message;

                return View(register);
            }

            return RedirectToAction(
                          "Login",
                          "Auth",
                          new { area = "Auth" });


        }

        // DELIVERY REGISTER PAGE
        [HttpGet]
        public IActionResult RegisterDeliveryBoy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDeliveryBoy(RegisterDeliveryBoyViewModel register)
        {
            var response =
                           await _apiService
                           .PostAsync<
                               RegisterDeliveryBoyViewModel,
                               AuthResponseViewModel>(
                               "api/Auth/register-Delivery-Boy",
                               register);

            if (!response.Success)
            {
                ViewBag.Error =
                    response.Message;

                return View(register);
            }

            return RedirectToAction(
                         "Login",
                         "Auth",
                         new { area = "Auth" });
        }
        // SHOP OWNER REGISTER PAGE
        [HttpGet]
        public IActionResult RegisterShopOwner()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterShopOwner( RegisterShopOwnerViewModel model)
        {
            var response =
                await _apiService
                .PostAsync<
                    RegisterShopOwnerViewModel,
                    AuthResponseViewModel>(
                    "api/Auth/register-shop-owner",
                    model);

            if (!response.Success)
            {
                ViewBag.Error =
                    response.Message;

                return View(model);
            }

            return RedirectToAction(
                         "Login",
                         "Auth",
                         new { area = "Auth" });
        }
    }
}
