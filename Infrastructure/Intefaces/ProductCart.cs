using Core.DTO;

namespace Infrastructure.Intefaces
{
    public interface IProductCartInteface<TEntity, TDto>
    {
        Task<TEntity> AddItemToCart(ProductCartDTO productCartDTO);
        Task<TEntity> UpdateItemQuantity(ProductCartDTO productCartDTO);
        Task<TEntity> RemoveItemFromCart(ProductCartDTO productCartDTO);
        Task<TEntity> GetCartById(ProductCartDTO productCartDTO);


    }
}
