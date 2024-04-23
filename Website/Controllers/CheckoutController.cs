using Microsoft.AspNetCore.Mvc;

namespace PosWebsite.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
        public IActionResult CreditCard()
        {
            return View();
        }
        public IActionResult Bank()
        {
            return View();
        }
        public IActionResult Paypal()
        {
            return View();
        }
        public IActionResult Cash()
        {
            return View();
        }
        public IActionResult PaymentSuccess()
        {
            return View();
        }
    }
}
