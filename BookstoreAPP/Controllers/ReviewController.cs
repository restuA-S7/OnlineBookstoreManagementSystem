using BookstoreAPP.DAL;
using BookstoreAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPP.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReview _review;
        public ReviewController(IReview review)
        {
            _review = review;
        }
        public IActionResult Index()
        {
            //TempData["OrderId"]TempData["BookId"]
            TempData.Keep("OrderId");
            TempData.Keep("BookId");
            ViewBag.OrderId = TempData["OrderId"];
            ViewBag.BookId = TempData["BookId"];
            return View();
        }

        public IActionResult ReviewForm() {
            TempData.Keep("OrderId");
            TempData.Keep("BookId");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Review review){
            
            _review.Add(new Review
            {
                BookId = Convert.ToInt32(TempData["BookId"]),
                CustomerId=1,
                Rating=review.Rating,
                Comment=review.Comment,
            });
            
            return RedirectToAction("Index","Home");
        }

    }
}
