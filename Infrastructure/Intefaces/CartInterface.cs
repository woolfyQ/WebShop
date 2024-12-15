using Core.DTO;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace Infrastructure.Intefaces
{
    public interface ICartInterface<TEntity, TDto>
    {
        Task<TEntity> AddItemToCart(CartDTO cartDTO, ItemCartDTO itemCartDTO, CancellationToken cancellationToken);

        Task<TEntity> UpdateItemQuantity(ItemCartDTO itemCartDTO, int amount);

        Task<TEntity> RemoveItemFromCart(Guid cartId, Guid productId);

        Task<TEntity> CreateCart(TDto dto);

        Task<TEntity> DeleteCart(TDto dto);

        Task<TEntity> UpdateCart(TDto dto);

        Task<TEntity> GetByIdAsync(Guid Id);

    }

}
