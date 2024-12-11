using Core.DTO;
using Core.Entity;
using Infrastructure.Data;
using Infrastructure.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserInterface<User, UserDTO>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(UserDTO dto, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password 
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User> Update(Guid id, UserDTO dto, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            user.Name = dto.Name;
            user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.Password = dto.Password; // Обновляем пароль без хэширования
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User> Delete(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return user;
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[GetByEmailAsync] Searching for email: {email}");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            Console.WriteLine(user == null
                ? $"[GetByEmailAsync] User not found for email: {email}"
                : $"[GetByEmailAsync] Found user: {user.Email}, Id: {user.Id}");
            return user;
        }

        public Task<User?> ValidateUser(string email, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
