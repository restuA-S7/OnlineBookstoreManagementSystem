using BookstoreAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPP.DAL
{
    public class OrderDetailsEF : IOrderDetail
    {
        private readonly AppDbContext _dbContext;
        public OrderDetailsEF(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public OrderDetail Add(OrderDetail entity)
        {
            _dbContext.OrderDetails.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _dbContext.OrderDetails.ToList();
        }

        public OrderDetail GetById(int id)
        {
            return _dbContext.OrderDetails.SingleOrDefault(od => od.OrderDetailId == id);
        }

        public OrderDetail Update(OrderDetail entity)
        {
            _dbContext.OrderDetails.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
