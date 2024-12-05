using Core;
using Core.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CartRepository : IRepository<Cart>
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Carts
                .Include(c => c.Products) 
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task Create(Cart entity, CancellationToken cancellationToken)
        {
            await _context.Carts.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Cart entity, CancellationToken cancellationToken)
        {
            _context.Carts.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Cart entity, CancellationToken cancellationToken)
        {
            _context.Carts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task Create(IEnumerable<Cart> entities, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(IEnumerable<Cart> entities, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(IEnumerable<Cart> entities, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cart>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Cart> IRepository<Cart>.GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
