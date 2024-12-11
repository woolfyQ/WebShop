namespace Infrastructure.Intefaces
{
    public interface IStorageProductInterface<TEntity, TDto>
    {
        Task<TEntity> Create(TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Update(TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Delete(Guid id, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
