using BookstoreAPP.Models;

namespace BookstoreAPP.DTO
{
    public class BookDetailViewModel
    {
        public BookDTO Book { get; set; }
        public List<ReviewDTO> ReviewDto { get; set; }
        public bool IsLoggedIn { get; set; }

    }
}
