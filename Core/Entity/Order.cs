namespace Core.Entity
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Cart Cart { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ICollection<ProductCart> Products { get; set; }


    }
}
