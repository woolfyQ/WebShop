﻿using Core.Entity;

namespace Core
{
    public record Id(Guid Value);
    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task Create(TEntity entity, CancellationToken cancellationToken);
        Task Create(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task Update(TEntity entity, CancellationToken cancellationToken);
        Task Update(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task Delete(TEntity entity, CancellationToken cancellationToken);
        Task Delete(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<TEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}