using Core.Entity;

namespace Core.DTO
{
    public class ItemCartDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
        public decimal Price {  get; set; }
        public decimal TotalPrice { get; set; }
        public int Amount { get; set; }
        public Product? Product { get; set; }

    }
}

