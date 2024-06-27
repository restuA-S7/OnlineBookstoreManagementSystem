using BookstoreAPP.Models;

namespace BookstoreAPP.DAL
{
    public interface IOrder : ICrud<Order>
    {
        object Where(Func<object, bool> value);
    }
}
