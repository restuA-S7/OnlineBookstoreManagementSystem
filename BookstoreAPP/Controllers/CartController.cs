using BookstoreAPP.DAL;
using BookstoreAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPP.Controllers
{
    public class CartController : Controller
    {
        private readonly IBook _book;
        private readonly IOrderDetail _orderDetail;
        private readonly IOrder _order;

        public CartController(IBook book, IOrderDetail orderDetail,IOrder order)
        {
            _book = book;
            _orderDetail = orderDetail;
            _order = order;
        }


        public IActionResult Index(int id)
        {
            var bookdata = _book.GetById(id);
            return View(bookdata);
        }

        [HttpPost]
        public IActionResult Create(OrderDetail orderDetail)
        {
            var orderNew = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = orderDetail.Price * orderDetail.Quantity
            };
            
            _order.Add(orderNew);
            _orderDetail.Add(orderDetail);

            TempData["Order"] = orderNew;
            TempData["OrderDetails"] = orderDetail;
            return RedirectToAction("Index","Review");

            //return View();
        }
    }
}
