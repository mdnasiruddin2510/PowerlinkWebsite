using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult AccountDetails()
        {
            return View();
        }
        public IActionResult BillingAddress()
        {
            return View();
        }
        public IActionResult Wishlist()
        {
            return View();
        }
        public IActionResult OrderHistory()
        {
            return View();
        }
        public IActionResult TrackingOrder()
        {
            return View();
        }
    }
}
