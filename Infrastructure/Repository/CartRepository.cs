using Core.Entity;
using Core.DTO;
using Infrastructure.Intefaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repository
{
    public class CartRepository : ICartInterface<Cart, CartDTO>
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

    
        public async Task<Cart> CreateCart(CartDTO cartDTO)
        {
            // Преобразуем DTO в сущность Cart
            var cart = new Cart
            {
                Id = cartDTO.Id,  // Генерация нового Id
                User = cartDTO.User,  // Присваиваем User из DTO
                TotalPrice = cartDTO.TotalPrice,  // Присваиваем TotalPrice
                Products = cartDTO.Products?.Select(p => new ProductCart
                {
                    Product = p.Product,  // Связываем с продуктом
                    Amount = p.Amount     // Присваиваем количество
                }).ToList()
            };

            // Добавляем сущность Cart в контекст
            await _context.Set<Cart>().AddAsync(cart);
            await _context.SaveChangesAsync();

            // Возвращаем сущность Cart после сохранения
            return cart;
        }

        // Получение Cart по ID с возвращением сущности
        public async Task<Cart> GetByIdAsync(Guid id)
        {
            var cart = await _context.Set<Cart>()
                .Include(c => c.Products)  // Подгружаем связанные продукты
                .ThenInclude(pc => pc.Product)  // Подгружаем сами продукты
                .FirstOrDefaultAsync(c => c.Id == id);

            return cart;  // Возвращаем сущность Cart
        }

        // Обновление Cart
        public async Task<Cart> UpdateCart(CartDTO cartDTO)
        {
            var existingCart = await _context.Set<Cart>().FindAsync(cartDTO.Id);
            if (existingCart == null)
            {
                throw new Exception($"Cart with id {cartDTO.Id} not found");
            }

            // Обновляем поля сущности Cart
            existingCart.User = cartDTO.User;
            existingCart.TotalPrice = cartDTO.TotalPrice;

            // Обновляем связанные продукты
            existingCart.Products = cartDTO.Products?.Select(p => new ProductCart
            {
                Product = p.Product,  // Обновляем продукт
                Amount = p.Amount     // Обновляем количество
            }).ToList();

            // Обновляем сущность в контексте
            _context.Set<Cart>().Update(existingCart);
            await _context.SaveChangesAsync();

            // Возвращаем обновленную сущность Cart
            return existingCart;
        }

        // Удаление Cart
        public async Task<Cart> DeleteCart(CartDTO cartDTO)
        {
            var existingCart = await _context.Set<Cart>().FindAsync(cartDTO.Id);
            if (existingCart == null)
            {
                throw new Exception($"Cart with id {cartDTO.Id} not found");
            }

            // Удаляем сущность Cart
            _context.Set<Cart>().Remove(existingCart);
            await _context.SaveChangesAsync();

            // Возвращаем удаленную сущность Cart
            return existingCart;
        }
    }
}
