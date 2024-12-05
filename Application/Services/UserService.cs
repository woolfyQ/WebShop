using Application.Intefaces;
using Core;
using Core.DTO;
using Core.Entity;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserService : IUserInterface<User, UserDTO>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IRepository<User> userRepository, ILogger<UserService> logger)
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
                    Password = userDTO.Password,
                };
                await _userRepository.Create(user, cancellationToken);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании пользователя.");
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

            // Обновление пароля только если он передан
            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            }

            await _userRepository.Update(user, cancellationToken);
            return user;
        }

        public async Task<User> Delete(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            await _userRepository.Delete(user, cancellationToken);
            return user;
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            return user; // Возвращаем null, если пользователь не найден
        }

        public async Task<User> ValidateUser(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null)
            {
                return null; // Пользователь не найден
            }

            Console.WriteLine($"Проверка пароля для пользователя {email}");
            Console.WriteLine($"Введенный пароль: {password}");
            Console.WriteLine($"Сохраненный хеш: {user.Password}");

            // Проверяем, соответствует ли введенный пароль хешированному паролю
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null; // Неверный пароль
            }

            return user; // Пароль верен
        }
    
        //private bool VerifyPassword(UserDTO userDTO, string storedPasswordHash)
        //{
        //    return BCrypt.Net.BCrypt.Verify(userDTO.Password, storedPasswordHash);
        //}

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
