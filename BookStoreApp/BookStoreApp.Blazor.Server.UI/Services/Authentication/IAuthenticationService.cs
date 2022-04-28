using BookStoreApp.Blazor.Server.UI.Services.Clients;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(UserLoginDTO loginModel);
        Task Logout();
    }
}