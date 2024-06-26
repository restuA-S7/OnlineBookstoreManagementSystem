using BookstoreAPP.Models;

namespace BookstoreAPP.DAL
{
    public class ReviewsEF : IReview
    {
        private readonly BookstoreDbContext _dbContext;
        public ReviewsEF(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Review Add(Review entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetByReviewId(int bookId)
        {
            var result = _dbContext.Reviews.Where(x => x.BookId == bookId).ToList();
            if (result == null || !result.Any())
            {
                throw new ArgumentException("No Reviews Found for the given review Id");
            }
            return result;
        }

        public Review GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Review Update(Review entity)
        {
            throw new NotImplementedException();
        }
    }
}
