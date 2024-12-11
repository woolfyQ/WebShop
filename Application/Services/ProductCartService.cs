using Core.Entity;
using Core.DTO;
using Infrastructure.Intefaces;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductCartService
    {
        private readonly IProductCartInteface<ProductCart, ProductCartDTO> _productCartRepository;

        public ProductCartService(IProductCartInteface<ProductCart, ProductCartDTO> productCartRepository)
        {
            _productCartRepository = productCartRepository;
        }

        // Добавление предмета в корзину
        public async Task<ProductCart> AddItemToCart(Guid cartId, ProductCartDTO productCartDTO)
        {
            if (cartId == Guid.Empty)
            {
                throw new ArgumentException("Cart not found");
            }

            var cart = await _productCartRepository.GetCartById(productCartDTO);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart does not exist for the given user.");
            }

            return await _productCartRepository.AddItemToCart(productCartDTO);
        }

        // Обновление количества предметов в корзине
        public async Task<ProductCart> UpdateItemQuantity(Guid cartId, Guid productId, int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            var productCartDTO = new ProductCartDTO
            {
                Id = productId,
                Cart = new Cart { Id = cartId }, 
                Amount = amount
                
            };

            return await _productCartRepository.UpdateItemQuantity(productCartDTO);
        }

        // Удаление предмета из корзины
        public async Task<ProductCart> RemoveItemFromCart(Guid cartId, Guid productId)
        {
            var productCartDTO = new ProductCartDTO
            {
                Id = productId,
                Cart = new Cart { Id = cartId }
            };

            return await _productCartRepository.RemoveItemFromCart(productCartDTO);
        }

      
    }
}
