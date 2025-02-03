using System.Net.Http.Json;
using Core.DTO;

namespace WebShop.Services
{
    public class OrderClientService
    {
        private readonly HttpClient _httpClient;

        public OrderClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Создание заказа
        public async Task<OrderDTO?> CreateOrder(OrderDTO orderDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("Order/Create", orderDTO);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderDTO>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка создания заказа: {response.StatusCode} - {error}");
                return null;
            }
        }

        // Получение заказа по ID
        public async Task<OrderDTO?> GetOrderById(Guid orderId)
        {
            var response = await _httpClient.GetAsync($"Order/GetOrderBy{orderId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderDTO>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка получения заказа: {response.StatusCode} - {error}");
                return null;
            }
        }

        // Обновление заказа
        public async Task<OrderDTO?> UpdateOrder(OrderDTO orderDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("Order/Update", orderDTO);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderDTO>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка обновления заказа: {response.StatusCode} - {error}");
                return null;
            }
        }

        // Удаление заказа
        public async Task<bool> DeleteOrder(Guid orderId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.DeleteAsync($"Order/Delete/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка удаления заказа: {response.StatusCode} - {error}");
                return false;
            }
        }
    }
}
