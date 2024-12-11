using Core.DTO;
using Core.Entity;
using Infrastructure.Data;
using Infrastructure.Intefaces;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductInterface<Product, ProductDTO>
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Create(ProductDTO dto, CancellationToken cancellationToken)
    {
        // Преобразуем DTO в сущность
        var product = new Product
        {
            Id = dto.Id,  // Генерируем новый Id, если это необходимо
            Name = dto.Name,
            Price = dto.Price,
            Img = dto.Img,
            Specs = dto.Specs,
            Description = dto.Description
        };

        // Добавляем сущность в контекст
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        // Возвращаем сущность
        return product;
    }

    public async Task<Product> Update(ProductDTO dto, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == dto.Id, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Product with id {dto.Id} not found");
        }

        // Обновляем данные сущности Product
        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Img = dto.Img;
        product.Specs = dto.Specs;
        product.Description = dto.Description;

        // Обновляем сущность в контексте
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);

        // Возвращаем сущность
        return product;
    }

    public async Task<Product> Delete(Guid id, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Product with id {id} not found");
        }

        // Удаляем сущность из контекста
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        // Возвращаем сущность
        return product;
    }

    public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Product with id {id} not found");
        }

        // Возвращаем сущность
        return product;
    }

    public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .ToListAsync(cancellationToken);

        // Возвращаем коллекцию сущностей
        return products;
    }
}
