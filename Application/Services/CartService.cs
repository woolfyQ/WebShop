using Application.Intefaces;
using Core;
using Core.DTO;
using Core.Entity;

namespace Application.Services
{
    public class CartService : ICartInterface<Cart, CartDTO>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;

        public CartService(IRepository<Cart> cartRepository, IRepository<Product> productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
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
            await _cartRepository.Create(cart, CancellationToken.None);
            return cart;
        }

        public async Task<Cart> GetCartByUserId(Guid userId, CartDTO cartDTO)
        {
            var carts = await _cartRepository.GetAll(CancellationToken.None);
            var cart = carts.FirstOrDefault(c => c.User.Id == userId);

            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not Found for user");
            }
            return cart;
        }

        public async Task<Cart> AddItemToCart(Guid cartId, Guid productId, decimal price, int quantity = 1, CancellationToken cancellationToken = default)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId, cancellationToken);
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = cartId,
                    TotalPrice = 0,
                    Products = new List<ProductCart>()
                };
                await _cartRepository.Create(cart, cancellationToken);
            }

            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            var existingProductCart = cart.Products.FirstOrDefault(pc => pc.Product.Id == productId);
            if (existingProductCart != null)
            {
                existingProductCart.Amount += quantity;
            }
            else
            {
                var productCart = new ProductCart
                {
                    Id = Guid.NewGuid(),
                    Product = product,
                    Amount = quantity
                };

                cart.Products.Add(productCart);
            }
            cart.TotalPrice += price * quantity;
            await _cartRepository.Update(cart, cancellationToken);

            return cart;
        }

        public async Task<Cart> DeleteCart(Guid cartId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId, CancellationToken.None);
            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not found");
            }
            await _cartRepository.Delete(cart, CancellationToken.None);
            return cart;
        }

        public async Task<Cart> GetByIdAsync(Guid id)
        {
            var cart = await _cartRepository.GetByIdAsync(id, CancellationToken.None);
            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not found");
            }
            return cart;
        }
    }
}
