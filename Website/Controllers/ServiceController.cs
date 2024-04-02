using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
