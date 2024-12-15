using Application.Services;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;   
        }


        [HttpPost("CreateCart")]
        public async Task<IActionResult> CreateCart([FromBody] CartDTO cartDTO)
        {
            if (cartDTO == null)
            {
                return BadRequest("Invalid cart data.");
            }

            var cart = await _cartService.CreateCart(cartDTO);
            return CreatedAtAction(nameof(GetCartById), new { id = cart.Id }, cart); 
        }

        [HttpPost("AddItemToCart")]
        public async Task<IActionResult> AddItemToCart([FromBody] ItemCartDTO itemCartDTO, CancellationToken cancellationToken)
        {
            if (itemCartDTO == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var cart = await _cartService.AddItemToCart(itemCartDTO.Cart, itemCartDTO, cancellationToken);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("GetCartItems/{id}")]
        public async Task<IActionResult> GetCartItems(Guid id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound($"Cart with ID {id} not found.");
            }

            // Возвращаем корзину с товарами
            var cartDTO = new CartDTO
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                Products = (ICollection<Core.Entity.ItemCart>)cart.Products.Select(p => new ItemCartDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.Product.Name,
                    Quantity = p.Amount,
                    Price = p.Product.Price
                }).ToList()
            };

            return Ok(cartDTO);
        }

        [HttpGet("GetCartBy/{id}")]
        public async Task<IActionResult> GetCartById(Guid id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound($"Cart with ID {id} not found.");
            }

            return Ok(cart);
        }

        // PUT: api/cart/{id}
        [HttpPut("UpdateCart/{id}")]
        public async Task<IActionResult> UpdateCart(Guid id, [FromBody] CartDTO cartDTO)
        {
            if (cartDTO == null || cartDTO.Id != id)
            {
                return BadRequest("Cart ID mismatch.");
            }

            var cart = await _cartService.UpdateCart(cartDTO);
            return Ok(cart);
        }

        // DELETE: api/cart/{id}
        [HttpDelete("DeleteCart/{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var cartDTO = new CartDTO { Id = id }; // Мы передаем только ID для удаления
            var cart = await _cartService.DeleteCart(cartDTO);
            return NoContent(); 
        }
    }
}