using Core.DTO;
using Core.Entity;
using Infrastructure.Intefaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserService : IUserInterface<User,UserDTO>
    {
        private readonly IUserInterface<User, UserDTO> _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserInterface<User, UserDTO> userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
         
        }

        public async Task<User> Create(UserDTO userDTO, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = userDTO?.Name,
                    Email = userDTO.Email,
                    Password = userDTO.Password 
                   
                };
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

                await _userRepository.Create(user, cancellationToken);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user.");
                throw;
            }
        }

        public async Task<User> Update(Guid id, UserDTO userDTO, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            user.Name = userDTO?.Name;
            user.Email = userDTO.Email;

            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            }

            await _userRepository.Update(user.Id, userDTO, cancellationToken);
            return user;
        }

        public async Task<User> Delete(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            await _userRepository.Delete(user.Id, cancellationToken);
            return user;
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByEmailAsync(email, cancellationToken);
        }

        public async Task<User> ValidateUser(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null)
            {
                Console.WriteLine("Serivce User not found");
                throw new Exception("User not found");

            }

            return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return user;
        }
    }
}
