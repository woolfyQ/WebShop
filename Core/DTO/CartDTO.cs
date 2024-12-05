using Core.Entity;

namespace Core.DTO
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual ICollection<ProductCart> Products { get; set; }


        public static implicit operator CartDTO(Cart cart) => new()
        {
            Id = cart.Id,
            User = cart.User,
            TotalPrice = cart.TotalPrice,
            Products = cart.Products
        };
    }
}
