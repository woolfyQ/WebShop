using Core.Entity;

namespace Core.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Cart Cart { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ICollection<ProductCart> Products { get; set; }



        public static implicit operator OrderDTO(Order order) => new()
        {
            Id = order.Id,
            User = order.User,
            Cart = order.Cart,
            TotalPrice = order.TotalPrice,
            DateTime = order.DateTime,
            Products = order.Products
        };
    }
}
