using BlazorApp.Client.Authentication.Models;
using BlazorApp.Shared.Authentication.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Authentication.Services
{
    public static class AuthenticationHelper
    {
        public static ClaimsPrincipal GetClaimsPrincipalFromClientPrincipal(ClientPrincipal clientPrincipal)
        {
            if (clientPrincipal is null)
            {
                return new ClaimsPrincipal();
            }

            try
            {
                clientPrincipal.UserRoles = clientPrincipal.UserRoles.Except(new string[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase);

                if (!clientPrincipal.UserRoles.Any())
                {
                    return new ClaimsPrincipal();
                }

                var identity = new ClaimsIdentity(clientPrincipal.IdentityProvider);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, clientPrincipal.UserId));
                identity.AddClaim(new Claim(ClaimTypes.Name, clientPrincipal.UserDetails));
                identity.AddClaims(clientPrincipal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));

                return new ClaimsPrincipal(identity);
            }
            catch
            {
                return new ClaimsPrincipal();
            }
        }
    }
}
