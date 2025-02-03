using Core.Entity;

namespace Core.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get;set; }
        public Guid CartId {  get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateTime { get; set; }


        public static implicit operator OrderDTO(Order order) => new()
        {
            Id = order.Id,
            UserId = order.UserId,
            CartId = order.CartId,
            TotalPrice = order.TotalPrice,
            DateTime = order.DateTime,
        };
    }
}
