namespace Infrastructure.Intefaces
{
    public interface IProductInterface<TEntity, TDto>
    {
        Task<TEntity> Create(TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Update(TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Delete(Guid Id, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);
    }
}
