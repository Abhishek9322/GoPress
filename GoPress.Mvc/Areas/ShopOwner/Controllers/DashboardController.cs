using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.ShopOwner.Controllers
{
    [Area("ShopOwner")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
