using BlazorApp.Client.Authentication.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using BlazorApp.Shared.Authentication.Services;
using System.Security.Principal;
using BlazorApp.Shared.Authentication.Models;

namespace BlazorApp.Client.Authentication.Services
{
    public class UserAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _client;

        public UserAuthenticationStateProvider(IWebAssemblyHostEnvironment environment)
        {
            _client = new HttpClient { BaseAddress = new Uri(environment.BaseAddress) };
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = await _client.GetFromJsonAsync<UserAuthenticationState>("/.auth/me");
            var clientPrincipal = state.ClientPrincipal;

            var claimsPrincipal = AuthenticationHelper.GetClaimsPrincipalFromClientPrincipal(clientPrincipal);

            return new AuthenticationState(new ClaimsPrincipal(claimsPrincipal));
        }
    }
}
