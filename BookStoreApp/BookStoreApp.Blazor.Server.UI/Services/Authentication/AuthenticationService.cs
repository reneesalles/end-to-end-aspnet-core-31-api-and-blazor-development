using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Clients;
using BookStoreApp.Blazor.Server.UI.Static;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IBookStoreClient _bookStoreClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IBookStoreClient bookStoreClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _bookStoreClient = bookStoreClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(UserLoginDTO loginModel)
        {
            var response = await _bookStoreClient.LoginAsync(loginModel);

            //Store token
            await _localStorage.SetItemAsStringAsync(LocalStorageConstants.AccessToken, response.Token);

            //Change auth state of app
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

            return true;
        }
    }
}