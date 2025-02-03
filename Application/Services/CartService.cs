using Core.DTO;
using Core.Entity;
using Infrastructure.Intefaces;

namespace Application.Services
{
    public class CartService : ICartInterface<Cart, CartDTO>
    {
        private readonly ICartInterface<Cart, CartDTO> _cartRepository;
        private readonly IProductInterface<Product, ProductDTO> _productRepository;

        public CartService(
            ICartInterface<Cart, CartDTO> cartRepository,
            IProductInterface<Product, ProductDTO> productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<Cart> AddItemToCart(Guid cartId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            if (cart == null) throw new Exception("Cart not found");

            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product == null) throw new Exception("Product not found");

            var existingProduct = cart.Products.FirstOrDefault(p => p.ProductId == productId);
            if (existingProduct != null)
            {
                existingProduct.Amount += amount;
            }
            else
            {
                var itemCart = new ItemCart
                {
                    CartId = cart.Id,
                    UserId = cart.UserId,
                    ProductId = productId,
                    Amount = amount,
                    Product = product,
                };
                cart.Products.Add(itemCart);
            }

            cart.TotalPrice = cart.Products.Sum(p => p.Amount * (p.Product?.Price ?? 0));
            await _cartRepository.UpdateCart(cart);
            return cart;
        }

        public async Task<Cart> UpdateItemQuantity(Guid cartId, Guid productId, int amount)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            if (cart == null) throw new InvalidOperationException("Корзина не найдена.");

            var product = cart.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null) throw new InvalidOperationException("Продукт не найден в корзине.");


            product.Amount += amount;

            cart.TotalPrice = cart.Products
                .Where(p => p.Product != null)
                .Sum(p => p.Amount * p.Product.Price);

            await _cartRepository.UpdateCart(cart);
            return cart;
        }

        public async Task<Cart> RemoveItemFromCart(Guid cartId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            if (cart == null) throw new Exception("Cart not found");

            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product == null) throw new Exception("Product not found");

            var productToRemove = cart.Products.FirstOrDefault(p => p.ProductId == productId);
            if (productToRemove == null) throw new Exception($"Product not found in cart: {productId}");

            if (productToRemove.Amount > amount)
            {
                productToRemove.Amount -= amount;
            }
            else
            {
                cart.Products.Remove(productToRemove);
            }

            cart.TotalPrice = cart.Products
                .Where(p => p.Product != null)
                .Sum(p => p.Amount * p.Product.Price);

            await _cartRepository.UpdateCart(cart);
            return cart;
        }

        public async Task<Cart> CreateCart(CartDTO cartDTO)
        {
            var cart = await _cartRepository.GetByIdAsync(cartDTO.Id);
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = cartDTO.UserId,
                    User = cartDTO.User,
                    TotalPrice = 0,
                    Products = new List<ItemCart>()
                };

                var createdCart = await _cartRepository.CreateCart(cart);
                return createdCart;
            }
            return cart;
        }

        public async Task<Cart> GetCartByUserId(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User = null,
                    TotalPrice = 0,
                    Products = new List<ItemCart>()
                };

                await _cartRepository.CreateCart(new CartDTO
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    TotalPrice = cart.TotalPrice ?? 0,
                    Products = new List<ItemCartDTO>()
                });
            }

            return cart;
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
            if (cart == null) throw new Exception($"Cart with id {cartDTO.Id} not found");

            cart.UserId = cartDTO.UserId;
            cart.TotalPrice = cartDTO.TotalPrice;

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
