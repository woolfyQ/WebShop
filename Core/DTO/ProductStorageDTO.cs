using Core.Entity;

namespace Core.DTO
{
    public class ProductStorageDTO
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; }



        public static implicit operator ProductStorageDTO(ProductStorage productStorage) => new()
        {
            Id = productStorage.Id,
            Amount = productStorage.Amount,
            Product = productStorage.Product,
        };
    }
}
