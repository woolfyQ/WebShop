using ShopAPI.AuthResult;
using ShopAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ShopAPI.Service
{
    public class AuthService : IAuth
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var responce = await _httpClient.PostAsJsonAsync("/api/Auth/Register", registerModel);

            if (!responce.IsSuccessStatusCode)
            {
                var errorcontent = await responce.Content.ReadAsStringAsync();
                Console.WriteLine("Error" + errorcontent);
                return new RegisterResult { Success = false, Error = "Server error" };
            }

            var result = await responce.Content.ReadFromJsonAsync<RegisterResult>();
            Console.WriteLine("Register Done");


            return result!;

        }

        public async Task<LoginResult> SignIn(LoginModel loginModel)
        {
            Console.WriteLine($"[UI SignIn] Sending Email: {loginModel.Email}, Password: {loginModel.Password}");

            var response = await _httpClient.PostAsJsonAsync("/api/Auth/SignIn", loginModel);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[UI SignIn] Server error: {response.ReasonPhrase}, Details: {errorContent}");
                return new LoginResult { Success = false, Error = "Server error: " + errorContent };
            }

            var responseContent = await response.Content.ReadFromJsonAsync<LoginResult>();
            Console.WriteLine($"[UI SignIn] Parsed response: Success={responseContent?.Success}, Token={responseContent?.Token}");

            if (responseContent?.Success == true)
            {
                _httpContextAccessor.HttpContext.Session.SetString("authToken", responseContent.Token);
            }

            return responseContent!;
        }

        public async void Logout()
        {
            _httpContextAccessor.HttpContext.Session.Remove("authToken");  // удаление токена
        }
        public Guid? GetUserIdFromToken()
        {
            // Получаем токен из сессии
            var token = _httpContextAccessor.HttpContext.Session.GetString("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return null;  // Токен не найден, возможно, пользователь не авторизован
            }

            try
            {
                // Декодируем JWT токен
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    // Извлекаем UserId из claims
                    var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub"); // sub - стандартное имя для идентификатора пользователя
                    if (userIdClaim != null)
                    {
                        return Guid.Parse(userIdClaim.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при извлечении UserId из токена: {ex.Message}");
            }

            return null;  // Не удалось извлечь UserId
        }

    }
}