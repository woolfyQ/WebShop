using Application.Services;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("Order")]

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
            return CreatedAtAction(nameof(GetOrder), new { id = cart.Id }, cart);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            var order = await _orderService.Update(orderDTO,cancellationToken);

            return Ok(order);
        }




        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] CancellationToken cancellationToken)
        {
            var cart = await _orderService.Delete(id, cancellationToken);
            return Ok(cart);
        }
  


        [HttpGet("GetOrderBy{id}")]
        public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetByIdAsync(id, cancellationToken);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }


    }
}
