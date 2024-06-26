using BookstoreAPP.Models;

namespace BookstoreAPP.DAL
{
    public class OrderDetailsEF : IOrderDetail
    {
        private readonly BookstoreDbContext _dbContext;

        public OrderDetailsEF(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public OrderDetail Add(OrderDetail entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetById(int id)
        {
            throw new NotImplementedException();
        }

        public OrderDetail Update(OrderDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
