using Application.Intefaces;
using Core;
using Core.DTO;
using Core.Entity;

namespace Application.Services
{
    public class StorageService : IStorageProductInterface<ProductStorage, ProductStorageDTO>
    {
        private readonly IRepository<ProductStorage> _storageRepository;
        public StorageService(IRepository<ProductStorage> ProductStorage)
        {
            _storageRepository = ProductStorage;
        }

        public async Task<ProductStorage> Create(ProductStorageDTO ProductStorageDTO)
        {
            var productStorage = new ProductStorage
            {
                Id = Guid.NewGuid(),
                Amount = 0,
                Product = ProductStorageDTO.Product,
            };
            await _storageRepository.Create(productStorage, CancellationToken.None);
            return productStorage;
        }
        public async Task<ProductStorage> Update(Guid Id, ProductStorageDTO ProductStorageDTO,CancellationToken cancellationToken)
        {
            var productStorage = await _storageRepository.GetByIdAsync(Id, cancellationToken);
            if (productStorage == null)
            {
                throw new Exception("ProductInStorage not found");
            }
            productStorage.Amount = ProductStorageDTO.Amount;
            productStorage.Product = ProductStorageDTO.Product;

            await _storageRepository.Update(productStorage, cancellationToken);
            return productStorage;
        }


        public async Task<ProductStorage> Delete(Guid Id,CancellationToken cancellationToken)
        {
            var productStorage = await _storageRepository.GetByIdAsync(Id, cancellationToken);
            if (productStorage == null)
            {
                throw new Exception("ProductInStorage not found");
            }
            await _storageRepository.Delete(productStorage, cancellationToken);
            return productStorage;
        }
        public async Task<ProductStorage> AddProduct(ProductStorageDTO ProductStorageDTO, int amount,CancellationToken cancellationToken)
        {
            var productStorage = await _storageRepository.GetByIdAsync(ProductStorageDTO.Id,cancellationToken);
            if (productStorage == null)
            {
                throw new Exception("Null");
            }
            productStorage.Amount += amount;
            await _storageRepository.Update(productStorage, cancellationToken);
            return productStorage;

        }

        public async Task<ProductStorage> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var ProductStorage = await _storageRepository.GetByIdAsync(Id,cancellationToken);
            {
                if (ProductStorage == null)
                {
                    throw new Exception("Storage not found");
                }
            }
            return ProductStorage;
        }
    }
}
