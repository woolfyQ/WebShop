using Core.DTO;
using Core.Entity;
using Infrastructure.Intefaces;

namespace Application.Services
{
    public class ProductService : IProductInterface<Product, ProductDTO>
    {
        private readonly IProductInterface<Product, ProductDTO> _productRepository;

        public ProductService(IProductInterface<Product, ProductDTO> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Create(ProductDTO productDTO, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Description = productDTO.Description,
                Name = productDTO.Name,
                Img = productDTO.Img,
                Price = productDTO.Price,
                Specs = productDTO.Specs,
            };

            await _productRepository.Create(productDTO, cancellationToken);
            return product;
        }

        public async Task<Product> Update(ProductDTO productDTO, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(productDTO.Id, cancellationToken)
                          ?? throw new KeyNotFoundException("Product not found");

            product.Description = productDTO.Description;
            product.Name = productDTO.Name;
            product.Img = productDTO.Img;
            product.Price = productDTO.Price;
            product.Specs = productDTO.Specs;

            await _productRepository.Update(productDTO, cancellationToken);
            return product;
        }

        public async Task<Product> Delete(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken)
                          ?? throw new KeyNotFoundException("Product not found");

            await _productRepository.Delete(id, cancellationToken);
            return product;
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdAsync(id, cancellationToken)
                   ?? throw new KeyNotFoundException("Product not found");
        }

        public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAll(cancellationToken);
            return products.Any() ? products : throw new KeyNotFoundException("No products found");
        }
    }
}
