using Core.DTO;

namespace Infrastructure.Intefaces
{
    public interface IStorageInterface<TEntity, TDto>
    {
        Task<TEntity> Create(TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Update(TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Delete(TDto dto, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
