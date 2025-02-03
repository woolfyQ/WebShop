using Core.Entity;
using Core.DTO;
using Infrastructure.Intefaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CartRepository : ICartInterface<Cart, CartDTO>
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByUserId(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.Products)
                .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart> CreateCart(CartDTO cartDTO)
        {
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = cartDTO.UserId,
                TotalPrice = cartDTO.TotalPrice,
                Products = new List<ItemCart>()
            };

            await _context.Set<Cart>().AddAsync(cart);
            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> GetByIdAsync(Guid id)
        {
            var cart = await _context.Set<Cart>()
                .Include(c => c.Products)
                .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
            return cart;
        }

        public async Task<Cart> UpdateCart(CartDTO cartDTO)
        {
            var existingCart = await _context.Set<Cart>().FindAsync(cartDTO.Id);

            _context.Set<Cart>().Update(existingCart);
            await _context.SaveChangesAsync();

            return existingCart;
        }

        public async Task<Cart> DeleteCart(CartDTO cartDTO)
        {
            var existingCart = await _context.Set<Cart>().FindAsync(cartDTO.Id);

            _context.Set<Cart>().Remove(existingCart);
            await _context.SaveChangesAsync();

            return existingCart;
        }

        public Task<Cart> AddItemToCart(Guid cartId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> UpdateItemQuantity(ItemCartDTO itemCartDTO, int amount)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> RemoveItemFromCart(Guid cartId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> UpdateItemQuantity(Guid cartId, Guid productId, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
