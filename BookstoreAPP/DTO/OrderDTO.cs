namespace BookstoreAPP.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
