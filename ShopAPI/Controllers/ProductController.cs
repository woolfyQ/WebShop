using Application.Services;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _productService.Create(productDTO, cancellationToken);

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

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var products = await _productService.GetAll(cancellationToken);
            return Ok(products);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ProductDTO productDTO,CancellationToken cancellationToken)
        { 
           var updatedProduct = await _productService.Update(productDTO, cancellationToken);

           return Ok(updatedProduct);
   
        }

        [HttpDelete("DeleteBy/{id}")]
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
