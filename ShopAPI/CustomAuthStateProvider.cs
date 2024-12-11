using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShopAPI
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Constans.JWTToken))

                { return await Task.FromResult(new AuthenticationState(anonymous)); }

                var getUserClaims = DecryptToken(Constans.JWTToken);
                if (getUserClaims != null)
                {
                    return await Task.FromResult(new AuthenticationState(anonymous));
                }
                var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch { return await Task.FromResult(new AuthenticationState(anonymous)); }
        }



        public async void UpdateAuthetication(string JwtToken)
        {
            var claims = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(JwtToken))
            {
                Constans.JWTToken = JwtToken;
                var getUSerClaims = DecryptToken(JwtToken);
                claims = SetClaimPrincipal(getUSerClaims);
            }
           else
            {
                Constans.JWTToken = null!;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
          
        }





        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new (ClaimTypes.Email, claims.Email!)
                }, "JwtAuth"));
        }

        private static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (!string.IsNullOrEmpty(jwtToken)) { return new CustomUserClaims(); }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var email = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email);
            return new CustomUserClaims(email!.Value);
        }
    }
}
    

