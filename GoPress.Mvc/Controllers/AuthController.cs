using GoPress.Mvc.Models.Auth;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace GoPress.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel login)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }

            var responce= 
                await _apiService.PostAsync<LoginViewModel,AuthResponseViewModel>("api/auth/All-Login", login);

            if(!responce.Success)
            {
                ViewBag.Error=responce.Message;
                return View(login);
            }
            Response.Cookies.Append(
                "AuthToken",
                responce.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires =
                        DateTimeOffset.UtcNow
                            .AddDays(1)
                });
            switch (responce.Role)
            {
                case "Customer":
                    return RedirectToAction(
                        "Customer",
                        "Dashboard");

                case "DeliveryBoy":
                    return RedirectToAction(
                        "DeliveryBoy",
                        "Dashboard");

                case "ShopOwner":
                    return RedirectToAction(
                        "ShopOwner",
                        "Dashboard");

                case "Admin":
                    return RedirectToAction(
                        "Admin",
                        "Dashboard");

                default:
                    return RedirectToAction(
                        "Index",
                        "Home");
            }

        }

        //Logout
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");

            return RedirectToAction(
                "Login");
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

            return RedirectToAction("Login");

            
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
                               "api/Auth/register-delivery-boy",
                               register);

            if (!response.Success)
            {
                ViewBag.Error =
                    response.Message;

                return View(register);
            }

            return RedirectToAction("Login");
        }
        // SHOP OWNER REGISTER PAGE
        [HttpGet]
        public IActionResult RegisterShopOwner()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>
          RegisterShopOwner(
              RegisterShopOwnerViewModel model)
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

            return RedirectToAction("Login");
        }
    }
}
