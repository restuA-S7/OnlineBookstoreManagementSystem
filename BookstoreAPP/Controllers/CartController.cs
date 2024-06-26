//using BookstoreAPP.DAL;
//using BookstoreAPP.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BookstoreAPP.Controllers
//{
//    public class CartController : Controller
//    {
//        private readonly IBook _book;

//        public CartController(IBook book)
//        {
//            _book = book;
//        }


//        public IActionResult Index(int id) 
//        {
//            var bookdata = _book.GetById(id);
//            return View(bookdata);
//        }

//        [HttpPost]
//        public IActionResult Create(OrderDetail orderDetail)
//        {

//            return View();
//        }
//    }
//}
