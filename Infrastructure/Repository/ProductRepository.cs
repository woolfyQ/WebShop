using Core;
using Core.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Product entity, CancellationToken cancellationToken)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.Products.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Create(IEnumerable<Product> entities, CancellationToken cancellationToken)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
            await _context.Products.AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Product entity, CancellationToken cancellationToken)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Products.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(IEnumerable<Product> entities, CancellationToken cancellationToken)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
            _context.Products.UpdateRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Product entity, CancellationToken cancellationToken)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(IEnumerable<Product> entities, CancellationToken cancellationToken)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
            _context.Products.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(Guid Id,CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(Id,cancellationToken);
        }

        public Task<Product> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
    }
}