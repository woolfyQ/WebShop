using Core.DTO;
using Core.Entity;
using Infrastructure.Intefaces;

namespace Application.Services
{
    public class StorageService : IStorageInterface<Storage, StorageDTO>
    {
        private readonly IStorageInterface<Storage, StorageDTO> _storageRepository;

        public StorageService(IStorageInterface<Storage, StorageDTO> storageRepository)
        {
            _storageRepository = storageRepository;
        }


        public async Task<Storage>Create(StorageDTO storageDTO, CancellationToken cancellationToken)
        {
            var storage = new Storage();
            {
                storage.Id = Guid.NewGuid();
                storage.Name = storageDTO.Name;
            }
            await _storageRepository.Create(storage, cancellationToken);
            return storage;

        }
        public async Task<Storage>Update(StorageDTO storageDTO, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetByIdAsync(storageDTO.Id, cancellationToken);

                storage.Id = storageDTO.Id;
                storage.Name = storageDTO.Name;

            await _storageRepository.Update(storage, cancellationToken);

            return storage;
        }
        public async Task<Storage>Delete(StorageDTO storageDTO, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetByIdAsync(storageDTO.Id, cancellationToken);
            if (storage == null)
            {
                throw new Exception("Storage not found");
            }
            if (storage != null)
            {
                await _storageRepository.Delete(storage, cancellationToken);
            }
            return storage;
        }

        public async Task<Storage> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetByIdAsync(id, cancellationToken);
            if (storage == null)
            {
                throw new KeyNotFoundException($"Storage with ID {id} not found.");
            }
            return storage;
        }



    }
}
