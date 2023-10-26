using BlazorApp.Shared.Authentication.Models;

namespace BlazorApp.Client.Authentication.Models
{
    public class UserAuthenticationState
    {
        public ClientPrincipal? ClientPrincipal { get; set; }
    }
}