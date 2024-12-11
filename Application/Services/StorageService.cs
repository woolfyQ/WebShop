//using Core;
//using Core.DTO;
//using Core.Entity;
//using Infrastructure.Intefaces;

//namespace Application.Services
//{
//    public class StorageService : IStorageProductInterface<Storage, StorageDTO>
//    {
//        private readonly IRepository<ProductStorage> _storageRepository;

//        public StorageService(IRepository<ProductStorage> storageRepository)
//        {
//            _storageRepository = storageRepository;
//        }

//        public async Task<ProductStorage> Create(ProductStorageDTO dto, CancellationToken cancellationToken)
//        {
//            var productStorage = new ProductStorage
//            {
//                Id = Guid.NewGuid(),
//                Amount = dto.Amount,
//                Product = dto.Product
//            };
//            await _storageRepository.Create(productStorage, cancellationToken);
//            return productStorage;
//        }

//        public async Task<ProductStorage> Update(ProductStorageDTO dto, CancellationToken cancellationToken)
//        {
//            var productStorage = await _storageRepository.GetByIdAsync(dto.Id, cancellationToken);
//            if (productStorage == null)
//            {
//                throw new Exception("ProductStorage not found");
//            }

//            productStorage.Amount = dto.Amount;
//            productStorage.Product = dto.Product;

//            await _storageRepository.Update(productStorage, cancellationToken);
//            return productStorage;
//        }

//        public async Task<ProductStorage> Delete(Guid id, CancellationToken cancellationToken)
//        {
//            var productStorage = await _storageRepository.GetByIdAsync(id, cancellationToken);
//            if (productStorage == null)
//            {
//                throw new Exception("ProductStorage not found");
//            }

//            await _storageRepository.Delete(productStorage, cancellationToken);
//            return productStorage;
//        }

//        public async Task<ProductStorage> GetByIdAsync(Guid id, CancellationToken cancellationToken)
//        {
//            var productStorage = await _storageRepository.GetByIdAsync(id, cancellationToken);
//            if (productStorage == null)
//            {
//                throw new Exception("ProductStorage not found");
//            }

//            return productStorage;
//        }
//    }
//}
