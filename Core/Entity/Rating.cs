namespace Core.Entity
{
    public class Rating : IEntity
    {
        
        public Guid Id { get; set; }
        public double Rate { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }

    }
}
