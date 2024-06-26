using BookstoreAPP.DAL;
using BookstoreAPP.DTO;
using BookstoreAPP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookstoreAPP.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBook _booksEF;
        private readonly IReview _reviewEF;
        public SearchController(IBook booksEF, IReview reviewEF)
        {
            _booksEF = booksEF;
            _reviewEF = reviewEF;
        }
        // GET: SearchController
        public ActionResult Index(string title)
        {
            var books = _booksEF.GetByBookTitle(title);
            return View(books);
        }

        // GET: SearchController/Details/5
        public ActionResult Details(int id)
        {
            var book = _booksEF.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            var reviews = _reviewEF.GetByReviewId(id);

            var listrevdto = new List<ReviewDTO>();

            foreach (var review in book.Reviews)
            {
                listrevdto.Add(new ReviewDTO
                {
                    Rating = review.Rating,
                    Comment = review.Comment,
                    Customer = new CustomerDTO
                    {
                        Name = review.Customer.Name,
                    }
                });
            }

            var viewModel = new BookDetailViewModel
            {
                Book = new BookDTO
                {
                    Title = book.Title,
                    BookId = book.BookId,
                    Author = book.Author,
                    Price = book.Price,
                    Stock = book.Stock
                },
                ReviewDto = listrevdto,
                IsLoggedIn = User.Identity.IsAuthenticated // Set IsLoggedIn based on authentication status
            };

            return View(viewModel);
        }

        // GET: SearchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
