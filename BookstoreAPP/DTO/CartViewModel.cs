namespace BookstoreAPP.DTO
{
    public class CartViewModel
    {
        public int OrderID { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
