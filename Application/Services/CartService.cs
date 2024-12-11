using Core.DTO;
using Core.Entity;
using Infrastructure.Intefaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Application.Services
{
    public class CartService : ICartInterface<Cart, CartDTO>
    {
        private readonly ICartInterface<Cart, CartDTO> _cartRepository;


        public CartService(ICartInterface<Cart, CartDTO> cartRepository)
        {
            _cartRepository = cartRepository;
            
        }

        public async Task<Cart> CreateCart(CartDTO cartDTO)
        {
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                User = cartDTO.User,
                TotalPrice = cartDTO.TotalPrice,
                Products = cartDTO.Products ?? new List<ProductCart>()
            };

            var createdCart = await _cartRepository.CreateCart(cart);

            return createdCart;
        }
        public async Task<Cart> GetByIdAsync(Guid Id)
        {
            var cart = await _cartRepository.GetByIdAsync(Id);
            if (cart == null)
            {
                throw new Exception($"Cart with id {Id} not found");
            }
            return cart;

        }
      
        public async Task<Cart> UpdateCart(CartDTO cartDTO)
        {
            var cart = await _cartRepository.GetByIdAsync(cartDTO.Id);

            cart.User = cartDTO.User;
            cart.TotalPrice = cartDTO.TotalPrice;
            cart.Products = cartDTO.Products;

            await _cartRepository.UpdateCart(cart);
            return cart;
        }
        public async Task<Cart> DeleteCart(CartDTO cartDTO)
        {
            var cart = await _cartRepository.GetByIdAsync(cartDTO.Id);
            await _cartRepository.DeleteCart(cart);
            return cart;
        }

    }
}
