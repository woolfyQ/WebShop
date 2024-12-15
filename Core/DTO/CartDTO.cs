using Core.Entity;

namespace Core.DTO
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual ICollection<ItemCart> Products { get; set; }


        public static implicit operator CartDTO(Cart cart) => new()
        {
            Id = cart.Id,
            User = cart.User,
            TotalPrice = cart.TotalPrice,
            Products = (ICollection<ItemCart>)cart.Products.Select(p => new ItemCartDTO
            {
                ProductId = p.ProductId,
                ProductName = p.Product.Name,
                Quantity = p.Amount,
                Price = p.Product.Price
            }).ToList()
        };
    }
}
