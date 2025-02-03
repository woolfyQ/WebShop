namespace Core.Entity
{
    public class User : IEntity
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public Cart Cart { get; set; }
    }
}
