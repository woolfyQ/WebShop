using Application.Intefaces;
using Core.DTO;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartInterface<Cart, CartDTO> _cartService;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartInterface<Cart, CartDTO> cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddToCartDTO addToCartDTO)
        {
            try
            {
                Guid cartId = GetCartIdFromCookies();
                _logger.LogInformation($"Добавление товара: CartId={cartId}, ProductId={addToCartDTO.ProductId}, Quantity={addToCartDTO.Quantity}");

                var updatedCart = await _cartService.AddItemToCart(cartId, addToCartDTO.ProductId, addToCartDTO.ProductPrice, addToCartDTO.Quantity);

                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении товара в корзину");
                return BadRequest(ex.Message);
            }
        }

        private Guid GetCartIdFromCookies()
        {
            string cartIdFromCookie = Request.Cookies["cartId"];

            if (string.IsNullOrEmpty(cartIdFromCookie) || !Guid.TryParse(cartIdFromCookie, out Guid cartId))
            {
                // Если cartId нет или он некорректен, генерируем новый
                cartId = Guid.NewGuid();
                Response.Cookies.Append("cartId", cartId.ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(30) });
            }

            return cartId;
        }
    }
}
