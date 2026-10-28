using Microsoft.AspNetCore.Mvc;

namespace GoPress.Mvc.Areas.ShopOwner.Controllers
{
    public class ShopOwnerProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
