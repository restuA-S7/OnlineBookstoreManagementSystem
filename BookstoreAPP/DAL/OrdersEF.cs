using BookstoreAPP.Models;

namespace BookstoreAPP.DAL
{
    public class OrdersEF : IOrder
    {
        private readonly BookstoreDbContext _dbContext;

        public OrdersEF(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Order Add(Order entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        private Order Json(Order entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public object Where(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
