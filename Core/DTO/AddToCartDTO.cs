
namespace Core.DTO
{
    public class AddToCartDTO
    {
        public Guid ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
