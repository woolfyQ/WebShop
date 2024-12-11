using Core.Entity;
using Core.DTO;
using Infrastructure.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class ProductCartRepository : IProductCartInteface<ProductCart, ProductCartDTO>
    {
        private readonly ApplicationDbContext _context;

        public ProductCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<ProductCart> GetCartByUserId(Guid userId, Guid cartId)
        //{
        //    return await _context.Set<ProductCart>()
        //        .Include(pc => pc.Cart)
        //        .Include(pc => pc.Product)
        //        .FirstOrDefaultAsync(pc => pc.Cart.UserId == userId && pc.Cart.Id == cartId);
        //}

        public async Task<ProductCart> AddItemToCart(ProductCartDTO productCartDTO)
        {
            var entity = new ProductCart
            {
                Id = Guid.NewGuid(),
                Cart = productCartDTO.Cart,
                Product = productCartDTO.Product,
                Amount = productCartDTO.Amount
            };

            await _context.Set<ProductCart>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ProductCart> UpdateItemQuantity(ProductCartDTO productCartDTO)
        {
            var entity = await _context.Set<ProductCart>()
                .FirstOrDefaultAsync(pc => pc.Cart.Id == productCartDTO.Cart.Id && pc.Product.Id == productCartDTO.Id);

            if (entity == null)
            {
                throw new InvalidOperationException("Item not found in the cart.");
            }

            entity.Amount = productCartDTO.Amount;
            _context.Set<ProductCart>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ProductCart> RemoveItemFromCart(ProductCartDTO productCartDTO)
        {
            var entity = await _context.Set<ProductCart>()
                .FirstOrDefaultAsync(pc => pc.Cart.Id == productCartDTO.Cart.Id && pc.Product.Id == productCartDTO.Id);

            if (entity == null)
            {
                throw new InvalidOperationException("Item not found in the cart.");
            }

            _context.Set<ProductCart>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ProductCart> GetCartById(ProductCartDTO productCartDTO)
        {
            return await _context.Set<ProductCart>()
                .Include(pc => pc.Cart)
                .Include(pc => pc.Product)
                .FirstOrDefaultAsync(pc => pc.Id == productCartDTO.Id);
        }
    }
}
