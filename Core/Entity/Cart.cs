namespace Core.Entity
{
    public class Cart : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal? TotalPrice { get; set; }
        public virtual ICollection<ItemCart>? Products { get; set; }
    }

}
