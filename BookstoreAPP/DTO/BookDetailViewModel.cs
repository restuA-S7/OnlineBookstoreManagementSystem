using BookstoreAPP.Models;

namespace BookstoreAPP.DTO
{
    public class BookDetailViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
