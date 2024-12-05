using ShopAPI.AuthResult;
using ShopAPI.Models;

namespace ShopAPI.Service
{
    public class AuthService : IAuth
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Auth/Register", registerModel);
            var response = await result.Content.ReadFromJsonAsync<RegisterResult>();
            return response!;
        }

        public async Task<LoginResult> SignIn(LoginModel loginModel)
        {
            Console.WriteLine($"Email: {loginModel.Email}, Password: {loginModel.Password}");

            var response = await _httpClient.PostAsJsonAsync("/api/Auth/SignIn", loginModel);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Server error: {response.ReasonPhrase}, Details: {errorContent}");
                return new LoginResult { Success = false, Error = "Server error: " + errorContent };
            }

            var responseContent = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (responseContent != null && responseContent.Success)
            {
                return responseContent;
            }
            else
            {
                return new LoginResult { Success = false, Error = "Неверные учетные данные" };
            }
        }
    }
}
