namespace BookstoreAPP.DTO
{
    public class ReviewViewModel
    {
        public int OrderID { get; set; }
        public List<ReviewItemViewModel> OrderDetails { get; set; }
        public int CustomerID { get; set; } // Tambahkan properti ini
    }
}
