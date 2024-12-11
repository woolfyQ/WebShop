using Core.DTO;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Intefaces;
using Application.Services;
using System.Text.Json;
using System.Security.Claims;
using ShopAPI.Token;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCartController : ControllerBase
    {
        private readonly ProductCartService _productCartService;
       
        public ProductCartController(ProductCartService productCartService)
        {
            _productCartService = productCartService;
           
        }

        [HttpPost("AddItemInCart")]
        public async Task<IActionResult> AddItemToCart(Guid cartId, [FromBody] ProductCartDTO productCartDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (productCartDTO == null)
            {
                return BadRequest("Invalid product data.");
            }
          
   
            var cart = await _productCartService.AddItemToCart(cartId, productCartDTO);
            return Ok(cart); 
        }

       
        [HttpPut("{cartId}/update/{productId}")]
        public async Task<IActionResult> UpdateItemQuantity(Guid cartId, Guid productId, int amount)
        {
            var cart = await _productCartService.UpdateItemQuantity(cartId, productId, amount);
            return Ok(cart); 
        }

       
        [HttpDelete("{cartId}/remove/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart(Guid cartId, Guid productId)
        {
            var cart = await _productCartService.RemoveItemFromCart(cartId, productId);
            return Ok(cart); 
        }
    }
}
