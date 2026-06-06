using GoPress.Mvc.Models.Dashboard;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GoPress.Mvc.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class DashboardController : Controller
    {

        public async Task<IActionResult>Index()
        { // Read JWT Cookie
            var token =
                Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            // Read Claims From JWT
            var handler =
                new JwtSecurityTokenHandler();

            var jwtToken =
                handler.ReadJwtToken(token);

            var role =
                jwtToken.Claims.FirstOrDefault(
                    x => x.Type == ClaimTypes.Role)
                    ?.Value;

            var email =
                jwtToken.Claims.FirstOrDefault(
                    x => x.Type == ClaimTypes.Email)
                    ?.Value;

            var name =
                jwtToken.Claims.FirstOrDefault(
                    x => x.Type == ClaimTypes.Name)
                    ?.Value;

            var model = new DashboardViewModel
            {
                FullName = name,

                Email = email,

                Role = role
            };

            return View(model);
        }
        
        public IActionResult Customer()
        {
            return View();
        }

        public IActionResult DeliveryBoy()
        {
            return View();
        }

        public IActionResult ShopOwner()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }
    }
}
