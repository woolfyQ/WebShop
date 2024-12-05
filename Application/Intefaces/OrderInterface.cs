namespace Application.Intefaces
{
    public interface IOrderInterface<TEntity, TDto>
    {
        Task<TEntity> Create(TDto dto);
        Task<TEntity> AddProduct(TDto dto,CancellationToken cancellationToken);
        Task<TEntity> Delete(Guid Id, TDto dto,CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    }
}
