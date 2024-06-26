using BookstoreAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPP.DAL
{
    public class OrdersEF : IOrder
    {
        private readonly AppDbContext _dbContext;
        public OrdersEF(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Order Add(Order entity)
        {
            _dbContext.Orders.Add(entity);
            _dbContext.SaveChanges();
            return entity;
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

        

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _dbContext.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Book)
            .Where(o => o.CustomerId == userId)
            .ToList();
        }

        public Order Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
