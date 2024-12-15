using Core.DTO;
using Core.Entity;
using Infrastructure.Intefaces;

namespace Application.Services
{
    public class CartService : ICartInterface<Cart, CartDTO>
    {
        private readonly ICartInterface<Cart, CartDTO> _cartRepository;
        private readonly IProductInterface<Product, ProductDTO> _productRepository;

        public CartService(ICartInterface<Cart, CartDTO> cartRepository, IProductInterface<Product,ProductDTO> productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }


        public async Task<Cart> AddItemToCart(CartDTO cartDTO, ItemCartDTO itemCartDTO, CancellationToken cancellationToken)
        {
            // Получаем корзину по ID
            var cart = await _cartRepository.GetByIdAsync(cartDTO.Id);

            // Если корзины нет, создаем новую
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = cartDTO.Id,
                    TotalPrice = 0,
                    Products = new List<ItemCart>()
                };
                await _cartRepository.CreateCart(cart);
            }

            // Получаем продукт
            var product = await _productRepository.GetByIdAsync(itemCartDTO.ProductId, cancellationToken)
                          ?? throw new Exception("Product not found");

            // Ищем существующий товар в корзине
            var existingProduct = cart.Products.FirstOrDefault(p => p.ProductId == itemCartDTO.ProductId);

            if (existingProduct != null)
            {
                // Обновляем количество товара
                existingProduct.Amount += itemCartDTO.Quantity;
            }
            else
            {
                // Добавляем новый товар в корзину
                var itemCart = new ItemCart
                {
                    Id = Guid.NewGuid(),
                    Product = product,
                    ProductId = itemCartDTO.ProductId,
                    Amount = itemCartDTO.Quantity
                };

                cart.Products.Add(itemCart);
            }

            // Обновляем общую цену корзины
            cart.TotalPrice = cart.Products.Sum(p => p.Amount * p.Product.Price);

            // Обновляем корзину в базе данных
            await _cartRepository.UpdateCart(cart);

            
            return new Cart
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                Products = (ICollection<ItemCart>)cart.Products.Select(p => new ItemCartDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.Product.Name,
                    Quantity = p.Amount,
                    Price = p.Product.Price
                }).ToList()
            };
        }

        public async Task<Cart> UpdateItemQuantity(ItemCartDTO itemCartDTO, int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
            }

            var cart = await _cartRepository.GetByIdAsync(itemCartDTO.Cart.Id)
                       ?? throw new KeyNotFoundException($"Cart with ID {itemCartDTO.Cart.Id} not found.");

            var item = cart.Products.FirstOrDefault(p => p.ProductId == itemCartDTO.ProductId);
            if (item == null)
            {
                throw new KeyNotFoundException($"Product with ID {itemCartDTO.ProductId} not found in the cart.");
            }

            item.Amount = amount;

            cart.TotalPrice = cart.Products.Sum(p => p.Amount * p.Product.Price);

            
            await _cartRepository.UpdateCart(cart);

            return cart;
        }

        public async Task<Cart> RemoveItemFromCart(Guid cartId, Guid productId)
        {
           var cart = await _cartRepository.GetByIdAsync(cartId);
           
            if (cart == null)
            {
                throw new Exception("Cart not found");
            }
            var productToRemove = cart.Products.FirstOrDefault(p => p.Id == productId);
            if (productToRemove == null)
            {
                throw new Exception("Product not found in cart");
            }
            cart.Products.Remove(productToRemove);
            await _cartRepository.UpdateCart(cart);
            return cart;
        }
        public async Task<Cart> CreateCart(CartDTO cartDTO)
        {
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                User = cartDTO.User,
                TotalPrice = cartDTO.TotalPrice,
                Products = cartDTO.Products ?? new List<ItemCart>()
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

