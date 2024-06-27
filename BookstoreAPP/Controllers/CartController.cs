using BookstoreAPP.DAL;
using BookstoreAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPP.Controllers
{
    public class CartController : Controller
    {
        private readonly BookstoreDbContext _dbContext;
        private readonly IBook _book;
        private readonly IOrderDetail _orderDetail;
        private readonly IOrder _order;
        private readonly ICustomer _customer;

        public CartController(IBook book, IOrderDetail orderDetail,IOrder order,ICustomer customer, BookstoreDbContext dbContext)
        {
            _book = book;
            _orderDetail = orderDetail;
            _order = order;
            _customer = customer;
            _dbContext = dbContext;
        }

        public IActionResult Index(int id)
        {
            var bookdata = _book.GetById(id);
            return View(bookdata);
        }

        [HttpPost]
        public IActionResult Create(OrderDetail orderDetail)
        {
            var datenow = DateTime.Now;

            var orderNew = new Order
            {
                CustomerId = 1,
                OrderDate = datenow,
                TotalAmount = orderDetail.Price * orderDetail.Quantity,
            };
            _order.Add(orderNew);


            //return Json(orderDetail);
            var orderUse = _dbContext.Orders.Where(ord => ord.OrderDate == datenow).ToList()[0];

            _orderDetail.Add(new OrderDetail
            {
                BookId = orderDetail.BookId,
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price,
                OrderId = orderUse.OrderId,
            });

            //TempData["Order"] = orderNew;
            //TempData["OrderDetails"] = orderDetail;
            TempData["OrderId"] = orderUse.OrderId;
            TempData["BookId"] = orderDetail.BookId;

            return RedirectToAction("Index","Review");

            //return View();
        }
    }
}
