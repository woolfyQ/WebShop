﻿using ShopAPI.AuthResult;
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
            var responce = await _httpClient.PostAsJsonAsync("/api/Auth/Register", registerModel);

            if (!responce.IsSuccessStatusCode)
            {
                var errorcontent = await responce.Content.ReadAsStringAsync();
                Console.WriteLine("Error" + errorcontent);
                return new RegisterResult{ Success = false, Error = "Server error"};
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

            if (responseContent != null && responseContent.Success)
            {
                return responseContent;
            }
            else
            {
                return new LoginResult { Success = false, Error = "Invalid response from server" };
            }
        }
    }
}
