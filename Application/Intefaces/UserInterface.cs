namespace Application.Intefaces
{
    public interface IUserInterface<TEntity, TDto>
    {
        Task<TEntity> Create(TDto dto,CancellationToken cancellationToken);
        Task<TEntity> Update(Guid Id, TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Delete(Guid Id, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<TEntity> GetByEmailAsync(string Email, CancellationToken cancellationToken);

    }
}
