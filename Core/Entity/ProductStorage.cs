namespace Core.Entity
{
    public class ProductStorage : IEntity
    {

        public Guid Id { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; }


    }
}
