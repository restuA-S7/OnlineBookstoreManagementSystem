using BookstoreAPP.DAL;
using BookstoreAPP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookstoreAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new LayoutViewModel
            {
                IsLoggedIn = User.Identity.IsAuthenticated,
                UserRole = User.IsInRole("Staff") ? "Staff" : "Customer"
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
