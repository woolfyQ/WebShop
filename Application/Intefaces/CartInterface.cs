namespace Application.Intefaces
{
    public interface ICartInterface<TEntity, TDto>
    {
        Task<TEntity> GetCartByUserId(Guid Id,TDto dto);
        Task<TEntity> AddItemToCart(Guid cartId, Guid productId, decimal price, int quantity = 1, CancellationToken cancellationToken = default);
        Task<TEntity> CreateCart(TDto dto);
        Task<TEntity> DeleteCart(Guid CartId);
        Task<TEntity> GetByIdAsync(Guid Id);
    }
}
