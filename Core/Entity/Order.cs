namespace Core.Entity
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateTime { get; set; }

    }
}
