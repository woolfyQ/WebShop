using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace Infrastructure.Intefaces
{
    public interface ICartInterface<TEntity, TDto>
    {
        Task<TEntity> CreateCart(TDto dto);

        Task<TEntity> DeleteCart(TDto dto);

        Task<TEntity> UpdateCart(TDto dto);

        Task<TEntity> GetByIdAsync(Guid Id);

    }

}
