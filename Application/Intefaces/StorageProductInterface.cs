namespace Application.Intefaces
{
    public interface IStorageProductInterface<TEntity, TDto>
    {
        Task<TEntity> Create(TDto Tdto);
        Task<TEntity> Update(Guid Id, TDto Tdto,CancellationToken cancellationToken);
        Task<TEntity> Delete(Guid Id, CancellationToken cancellationToken);
        Task<TEntity> AddProduct(TDto Tdto, int Amount, CancellationToken cancellationToken);

        Task<TEntity> GetByIdAsync(Guid Id, CancellationToken cancellationToken );
    }
}
