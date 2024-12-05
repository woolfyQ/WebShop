namespace Application.Intefaces
{
    public interface IProductInterface<TEntity, TDto>
    {
        Task<TEntity> Create(TDto dto);
        Task<TEntity> Update(Guid Id, TDto dto, CancellationToken cancellationToken );
        Task<TEntity> Delete(Guid Id, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid Id,CancellationToken cancellationToken);
    }
}
