using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
