using Core.Entity;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ShopAPI.Services
{
    public class TestSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TestSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SaveCartToSession(Cart cart)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                throw new InvalidOperationException("Session is not available.");
            }

            var cartJson = JsonSerializer.Serialize(cart);
            session.SetString("Cart", cartJson); // Метод SetString должен работать
        }

        public Cart? GetCartFromSession()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                throw new InvalidOperationException("Session is not available.");
            }

            var cartJson = session.GetString("Cart"); // Метод GetString должен работать
            return cartJson == null ? null : JsonSerializer.Deserialize<Cart>(cartJson);
        }

    }
}
