using GoPress.Mvc.Areas.Auth.Auth;
using GoPress.Mvc.Helpers;
using GoPress.Mvc.Models.Responses;
using GoPress.Mvc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

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
        //[HttpPost]
        //public async Task<IActionResult>Login(LoginViewModel login)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(login);
        //    }

        //    var response =
        //        await _apiService.PostAsync<
        //            LoginViewModel,
        //            AuthResponseViewModel>(
        //            "api/auth/All-Login",
        //            login);

        //    if (!response.Success)
        //    {
        //        ViewBag.Error = response.Message;
        //        return View(login);
        //    }

        //    if (string.IsNullOrWhiteSpace(response.AccessToken))
        //    {
        //        ViewBag.Error = "Access token was not returned from the API.";
        //        return View(login);
        //    }

        //    _tokenService.SaveToken(response.AccessToken);

        //    var redirect =
        //        RoleRedirectHelper.GetRedirect(response.Role);

        //    return RedirectToAction(
        //        redirect.Action,
        //        redirect.Controller,
        //        new
        //        {
        //            area = redirect.Area
        //        });

        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
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
                ViewBag.Error ="Access token was not returned from the API.";

                return View(login);
            }

            // ==========================================
            // 1. Save JWT for API communication
            // ==========================================

            _tokenService.SaveToken(response.AccessToken);


            // ==========================================
            // 2. Create MVC authentication claims
            // ==========================================

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Email),
                new Claim(ClaimTypes.Email, login.Email),
                  new Claim(ClaimTypes.Role, response.Role)
             };


            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);


            var principal = new ClaimsPrincipal(identity);


            // ==========================================
            // 3. Sign user into MVC
            // ==========================================

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc =
                        DateTimeOffset.UtcNow.AddDays(1)
                });


            // ==========================================
            // 4. Redirect based on role
            // ==========================================

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
        //public IActionResult Logout()
        //{
        //    _tokenService.RemoveToken();

        //    return RedirectToAction(
        //        "Login",
        //        "Auth",
        //        new { area = "Auth" });
        //}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Remove JWT
            _tokenService.RemoveToken();

            // Remove MVC authentication
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

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
