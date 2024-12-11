using Core.Entity;

namespace Core.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
      


        public static implicit operator UserDTO(User user) => new()
        {
            Id = user.Id,
            Name = user?.Name,
            Email = user.Email,
            Password = user.Password,
        };
    }
}
