namespace Core.Entity
{
    public class ItemCart : IEntity
    {
        public Guid Id { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
      

    }

}
