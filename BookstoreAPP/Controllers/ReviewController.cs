using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPP.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
