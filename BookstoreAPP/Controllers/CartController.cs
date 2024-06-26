using BookstoreAPP.DAL;
using BookstoreAPP.DTO;
using BookstoreAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookstoreAPP.Controllers
{
    public class CartController : Controller
    {
        private readonly BooksEF _booksEF;
        private readonly AppDbContext _dbContext;

        public CartController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _booksEF = new BooksEF(dbContext); // Inisialisasi BooksEF dengan dbContext
        }

        // GET: Cart
        public IActionResult Index()
        {
            // Retrieve customer ID based on the logged-in user (this is just a placeholder, adjust as needed)
            int customerId = 1; // Placeholder for logged-in customer ID

            // Calculate TotalAmount for the new order
            var totalAmount = _dbContext.OrderDetails
                                        .Where(od => od.Order.CustomerId == customerId)
                                        .Sum(od => od.Quantity * od.Price);

            // Create a new order
            var newOrder = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount
            };

            // Add the new order to the database
            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();

            // Get the order ID of the newly created order
            var orderId = newOrder.OrderId;

            // Retrieve cart items
            var cartItems = _dbContext.OrderDetails
                                      .Include(od => od.Book)
                                      .Include(od => od.Order)
                                      .Where(od => od.OrderId == orderId) // Only include items from the new order
                                      .Select(od => new CartViewModel
                                      {
                                          OrderID = od.OrderId,
                                          Title = od.Book.Title,
                                          Quantity = od.Quantity,
                                          Price = od.Price,
                                          TotalAmount = od.Quantity * od.Price
                                      }).ToList();

            ViewBag.OrderID = orderId;
            ViewBag.TotalAmount = totalAmount;

            return View(cartItems);
        }


        public IActionResult Review(int orderId)
        {
            var order = _dbContext.Orders
                                  .Include(o => o.OrderDetails)
                                  .ThenInclude(od => od.Book)
                                  .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var reviewViewModel = new ReviewViewModel
            {
                OrderID = order.OrderId,
                CustomerID = order.CustomerId,
                OrderDetails = order.OrderDetails.Select(od => new ReviewItemViewModel
                {
                    BookID = od.BookId,
                    Title = od.Book.Title,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    TotalAmount = od.Quantity * od.Price
                }).ToList()
            };

            return View(reviewViewModel);
        }

        [HttpPost]
        public IActionResult SubmitReview(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model.OrderDetails)
                {
                    var review = new Review
                    {
                        BookId = item.BookID,
                        CustomerId = model.CustomerID,
                        Rating = item.Rating,
                        Comment = item.Comment
                    };
                    _dbContext.Reviews.Add(review);
                }

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Review", model);
        }


    }
}
