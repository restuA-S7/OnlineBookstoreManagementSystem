namespace BookstoreAPP.DTO
{
    public class ReviewItemViewModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public int Rating { get; set; } // Tambahkan properti ini
        public string Comment { get; set; } // Tambahkan properti ini
    }
}
