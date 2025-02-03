using Core.Entity;

namespace Core.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
        public string Specs { get; set; }
        public string Description { get; set; }


        public static implicit operator ProductDTO(Product product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Img = product.Img,
            Specs = product.Specs,
            Description = product.Description,
        };

    }
}
