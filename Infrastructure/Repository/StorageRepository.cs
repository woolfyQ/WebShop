using Core;
using Core.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class StorageRepository : IRepository<Storage>
{
    private readonly ApplicationDbContext _context;
        
    public StorageRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task Create(Storage storage,CancellationToken cancellationToken)
    {
        await _context.Storages.AddAsync(storage, cancellationToken);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(Storage storage,CancellationToken cancellationToken)
    {
            _context.Storages.Update(storage);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(Storage storage,CancellationToken cancellationToken)
    {
            _context.Storages.Remove(storage);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Storage>> GetAll(CancellationToken cancellationToken)
    {
        var storage = await _context.Storages.ToListAsync(cancellationToken);
        await _context.SaveChangesAsync();
        return storage;
    }
    public async Task<Storage> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var storage = await _context.Storages.
            FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return storage;
    }


}