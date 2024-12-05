using Application.Services;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI.Controllers
{
    [Route("Product")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _productService.Create(productDTO);

            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid Id,CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(Id, cancellationToken);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDTO productDTO,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedProduct = await _productService.Update(id, productDTO,cancellationToken);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var deletedProduct = await _productService.Delete(id, cancellationToken );
                return Ok(deletedProduct);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
