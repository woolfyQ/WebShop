using Core.DTO;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebShop.Services
{
    public class ProductCartClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ProductCartClientService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }
        public async Task<bool> AddProductToCart(Guid userId, Guid cartId, ProductCartDTO productCartDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/productcart/AddItemInCart", productCartDTO);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateProductQuantity(Guid cartId, Guid productId, int amount)
        {

            var response = await _httpClient.PutAsJsonAsync($"api/productcart/{cartId}/update/{productId}", amount);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> RemoveProductFromCart(Guid cartId, Guid productId)
        {

            var response = await _httpClient.DeleteAsync($"api/productcart/{cartId}/remove/{productId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;

        }








    }
}

