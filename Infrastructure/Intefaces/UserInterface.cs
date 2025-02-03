namespace Infrastructure.Intefaces
{
    public interface IUserInterface<TEntity, TDto>
    {
        Task<TEntity> Create(TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Update(Guid id, TDto dto, CancellationToken cancellationToken);
        Task<TEntity> Delete(Guid id, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<TEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<TEntity?> ValidateUser(string email, string password, CancellationToken cancellationToken);
    }
}
