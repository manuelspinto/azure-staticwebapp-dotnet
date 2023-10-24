using System;
using System.Linq;
using System.Net;
using Api.Authentication;
using BlazorApp.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ApiIsolated
{
    public class HttpTrigger
    {
        [Function("hello")]
        public HttpResponseData RunHello([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteStringAsync($"Hello from PUBLIC function");

            return response;
        }

        [Function("hello/protected")]
        public HttpResponseData RunProtectedHello([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var user = StaticWebAppsApiAuth.Parse(req);

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                var unauthResponse = req.CreateResponse(HttpStatusCode.Unauthorized);
                unauthResponse.WriteStringAsync($"User does does not have access to function");
                return unauthResponse;
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteStringAsync($"Hello '{user.Identity.Name}' from PROTECTED function");

            return response;
        }

        [Function("hello/protected/admin")]
        public HttpResponseData RunProtectedAdminHello([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteStringAsync($"Hello from ADMIN PROTECTED function");

            return response;
        }

        [Function("hello/protected/superadmin")]
        public HttpResponseData RunProtectedSuperAdminHello([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var user = StaticWebAppsApiAuth.Parse(req);

            if (user.Identity == null || !user.Identity.IsAuthenticated || !user.IsInRole("SuperAdmin"))
            {
                var unauthResponse = req.CreateResponse(HttpStatusCode.Unauthorized);
                unauthResponse.WriteStringAsync($"User does does not have access to function");
                return unauthResponse;
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteStringAsync($"Hello from SUPER ADMIN PROTECTED function");

            return response;
        }
    }
}
