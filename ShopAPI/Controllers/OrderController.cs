using Application.Services;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("Order")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await _orderService.Create(orderDTO);
            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = await _orderService.AddProduct(orderDTO,cancellationToken);
            return Ok(cart);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] CancellationToken cancellationToken)
        {
            var cart = await _orderService.Delete(id, cancellationToken);
            return Ok(cart);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart(Guid id,CancellationToken cancellationToken)
        {
            var cart = await _orderService.GetByIdAsync(id, cancellationToken);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
    }
}
