using BookstoreAPP.Models;

namespace BookstoreAPP.DTO
{
    public class ReviewDTO
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public CustomerDTO? Customer { get; set; }

    }
}
