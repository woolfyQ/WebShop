namespace Infrastructure.Intefaces
{
    public interface ICartInterface<TEntity, TDto>
    {


        Task<TEntity> AddItemToCart(Guid cartId, Guid productId, int amount, CancellationToken cancellationToken);

        Task<TEntity> GetCartByUserId(Guid Id);
        Task<TEntity> RemoveItemFromCart(Guid cartId, Guid productId, int amount, CancellationToken cancellationToken);
        Task<TEntity> UpdateItemQuantity(Guid cartId, Guid productId, int amount);

        Task<TEntity> CreateCart(TDto dto);

        Task<TEntity> DeleteCart(TDto dto);

        Task<TEntity> UpdateCart(TDto dto);

        Task<TEntity> GetByIdAsync(Guid Id);

    }

}
