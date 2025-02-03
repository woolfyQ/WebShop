namespace Core.Entity
{
    public class Product : IEntity
    {

        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Img { get; set; }

        public string Specs { get; set; }

        public string Description { get; set; }
        //public ICollection<ProductImage> ProductImages { get; set; }



    }
}
