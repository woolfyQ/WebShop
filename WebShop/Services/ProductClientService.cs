using Core.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebShop.Services
{
    public class ProductClientService
    {
        private readonly HttpClient _httpClient;

        public ProductClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Product/GetAllProducts");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>();
                }
                else
                {
                    
                    return Enumerable.Empty<ProductDTO>();  
                }
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                throw new Exception("Error fetching products.", ex);
            }
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ProductDTO>();
                }
                else
                {
                 
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                throw new Exception("Error fetching product by ID.", ex);
            }
        }
    }
}
