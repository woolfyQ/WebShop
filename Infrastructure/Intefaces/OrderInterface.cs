namespace Infrastructure.Intefaces
{
    public interface IOrderInterface<TEntity, TDto>
    {
        Task<TEntity> Create(TDto dto);
        Task<TEntity> Update(TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Delete(Guid id, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}