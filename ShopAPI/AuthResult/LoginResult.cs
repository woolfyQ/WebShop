﻿namespace ShopAPI.AuthResult
{
    public class LoginResult
    {
        public bool Success { get; set; }

        public string? Error { get; set; }

        public string? Token { get; set; }
    }
}
