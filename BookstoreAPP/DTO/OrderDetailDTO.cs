namespace BookstoreAPP.DTO
{
    public class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        public BookDTO Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
