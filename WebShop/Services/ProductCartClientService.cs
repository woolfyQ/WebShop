using Core.DTO;
using System.Text.Json;

namespace WebShop.Services
{
    public class ProductCartClientService
    {
        private readonly HttpClient _httpClient;

        public ProductCartClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartDTO?> GetCartByUserId(Guid userId)
        {
            var response = await _httpClient.GetAsync($"api/Cart/GetCartByUserId/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CartDTO>();
            }
            return null;
        }

        public async Task<bool> AddProductToCart(ItemCartDTO itemCartDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/cart/AddItemToCart", itemCartDTO);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemQuantity(Guid cartId, Guid productId, int amount)
        {
            try
            {
                var itemCartDTO = new ItemCartDTO
                {
                    CartId = cartId,
                    ProductId = productId,
                    Amount = amount
                };

                var response = await _httpClient.PostAsJsonAsync("api/Cart/ItemQuantity", itemCartDTO);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveProductFromCart(Guid cartId, Guid productId, int amount)
        {
            var response = await _httpClient.DeleteAsync($"api/cart/RemoveItemFromCart/{cartId}/{productId}/{amount}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateCart(CartDTO cartDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/cart/CreateCart", cartDTO);
            return response.IsSuccessStatusCode;
        }
    }
}
