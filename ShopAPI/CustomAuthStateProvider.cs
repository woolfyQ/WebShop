using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace ShopAPI
{
    public static class Constans
    {
        public static string JWTToken { get; set; } = "";
    }

    public record class CustomUserClaims(string Email);

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
       
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthStateProvider( IHttpContextAccessor httpContextAccessor)
        {
           
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("authToken");

            var identity = new ClaimsIdentity();
            if (!string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            }

            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = WebEncoders.Base64UrlDecode(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        public void MarkUserAsLoggedOut()
        {
            Console.WriteLine("Marking user as logged out");

            // Логика для очистки состояния аутентификации
           
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
            Console.WriteLine("User marked as logged out");
        }

        public async Task UpdateAuthentication(string jwtToken)
        {
            ClaimsPrincipal claimsPrincipal;

            if (!string.IsNullOrEmpty(jwtToken))
            {
                Constans.JWTToken = jwtToken;

                var userClaims = DecryptToken(jwtToken);
                if (userClaims != null && !string.IsNullOrEmpty(userClaims.Email))
                {
                    claimsPrincipal = SetClaimPrincipal(userClaims);
                }
                else
                {
                    claimsPrincipal = anonymous;
                }
            }
            else
            {
                Constans.JWTToken = null!;
                claimsPrincipal = anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                new(ClaimTypes.Email, claims.Email)
                }, "JwtAuth"));
        }

        private static CustomUserClaims? DecryptToken(string jwtToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtToken);

                // Логируем весь токен для диагностики
                Console.WriteLine($"Full Token: {jwtToken}");

                // Логируем все claims для диагностики
                foreach (var claim in token.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }

                // Попробуем найти email claim через другой метод
                var emailClaim = token.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email);
                if (emailClaim != null)
                {
                    Console.WriteLine($"Found email claim: {emailClaim.Value}");
                    return new CustomUserClaims(emailClaim.Value);
                }
                else
                {
                    Console.WriteLine("Email claim not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DecryptToken: {ex.Message}");
                return null;
            }
        }
    }

}