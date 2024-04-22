using Microsoft.AspNetCore.Mvc;

namespace PosWebsite.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
