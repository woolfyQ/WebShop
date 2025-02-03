namespace Core.Entity
{
    public class ItemCart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
        public int Amount { get; set; } 
        public Product? Product { get; set; }
       
      
    }

}
