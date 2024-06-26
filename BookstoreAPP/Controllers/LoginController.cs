using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPP.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logining() { 
        
            return View();
        }
    }
}
