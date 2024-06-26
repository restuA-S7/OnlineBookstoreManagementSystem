using System.Data.SqlTypes;

namespace BookstoreAPP.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public int Stock { get; set; }

        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }

    }
}
