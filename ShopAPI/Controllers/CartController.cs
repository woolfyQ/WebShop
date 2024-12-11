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
            return CreatedAtAction(nameof(GetCartById), new { id = cart.Id }, cart); // Возвращаем созданную корзину
        }

       
        [HttpGet("GetCartBy{id}")]
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
        [HttpPut("UpdateCart{id}")]
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
        [HttpDelete("DeleteCart{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var cartDTO = new CartDTO { Id = id }; // Мы передаем только ID для удаления
            var cart = await _cartService.DeleteCart(cartDTO);
            return NoContent(); 
        }
    }
}