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

        [HttpPut("ItemQuantity/{cartId}/{productId}/{amount}")]
        public async Task<IActionResult> UpdateItemQuantity(Guid cartId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _cartService.UpdateItemQuantity(cartId, productId, amount);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при обновлении количества товара: {ex.Message}");
            }
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
                var updatedCart = await _cartService.AddItemToCart(itemCartDTO.CartId, itemCartDTO.ProductId, itemCartDTO.Amount, cancellationToken);
                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("RemoveItemFromCart/{cartId}/{productId}/{amount}")]
        public async Task<IActionResult> RemoveItemFromCart(Guid cartId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCart = await _cartService.RemoveItemFromCart(cartId, productId, amount, cancellationToken);
                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("GetCartByUserId/{userId}")]
        public async Task<ActionResult<CartDTO>> GetCartByUserId(Guid userId)
        {
            var cart = await _cartService.GetCartByUserId(userId);
            if (cart == null)
            {
                return NotFound("Корзина не найдена.");
            }

            return Ok(cart);
        }

        [HttpGet("GetCartBy/{id}")]
        public async Task<IActionResult> GetCartById(Guid id)
        {
            try
            {
                var cart = await _cartService.GetByIdAsync(id);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return NotFound($"Cart with ID {id} not found: {ex.Message}");
            }
        }

        [HttpPut("UpdateCart/{id}")]
        public async Task<IActionResult> UpdateCart(Guid id, [FromBody] CartDTO cartDTO)
        {
            if (cartDTO == null || cartDTO.Id != id)
            {
                return BadRequest("Cart ID mismatch.");
            }

            try
            {
                var updatedCart = await _cartService.UpdateCart(cartDTO);
                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCart/{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            try
            {
                var cartDTO = new CartDTO { Id = id };
                var deletedCart = await _cartService.DeleteCart(cartDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Cart with ID {id} not found: {ex.Message}");
            }
        }
    }
}
