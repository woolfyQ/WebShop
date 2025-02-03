using Core.Entity;

namespace WebShop.Services
{
    public class EmailService
    {
        private readonly HttpClient _httpClient;
        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendEmail(UserMessage message)
        {
            var response = await _httpClient.PostAsJsonAsync("api/mail/sendmail", message);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error sending email: {errorMessage}");
                return false;
            }

        }


    }
}
