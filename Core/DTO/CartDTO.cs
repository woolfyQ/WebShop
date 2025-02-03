using Core.Entity;

namespace Core.DTO
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }  
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ItemCartDTO>? Products { get; set; }

        // Преобразование из сущности Cart в DTO Cart
        public static implicit operator CartDTO(Cart cart) => new()
        {
            Id = cart.Id,
            UserId = cart.UserId,  
            User = cart.User,
            TotalPrice = cart.TotalPrice ?? 0, 
            Products = cart.Products.Select(p => new ItemCartDTO
            {
                ProductId = p.ProductId,
                Amount = p.Amount
            }).ToList()
        };
    }
}