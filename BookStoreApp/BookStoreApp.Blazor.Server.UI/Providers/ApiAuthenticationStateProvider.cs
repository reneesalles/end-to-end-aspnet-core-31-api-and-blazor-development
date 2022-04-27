using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Static;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.UI.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _jwtSecurityTokenHandler = new();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());

            var savedToken = await _localStorage.GetItemAsStringAsync(LocalStorageConstants.AccessToken);
            if (!string.IsNullOrEmpty(savedToken))
            {
                var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
                if (tokenContent.ValidTo > DateTime.UtcNow)
                {
                    var claims = tokenContent.Claims;

                    user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                }
            }

            return new AuthenticationState(user);
        }

        public async Task LoggedIn()
        {
            var savedToken = await _localStorage.GetItemAsStringAsync(LocalStorageConstants.AccessToken);
            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims;
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public void LoggedOut()
        {
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}