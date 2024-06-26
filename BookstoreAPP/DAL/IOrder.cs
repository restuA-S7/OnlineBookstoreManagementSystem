using BookstoreAPP.Models;

namespace BookstoreAPP.DAL
{
    public interface IOrder: ICrud<Order>
    {
       
        IEnumerable<Order> GetOrdersByUserId(int userId);
    }
}
