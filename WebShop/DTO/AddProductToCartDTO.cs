namespace WebShop.DTO
{
    public class AddProductToCartDTO
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
