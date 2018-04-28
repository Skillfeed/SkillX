using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Skillx.Gateways.WebAPI.Options;

namespace Skillx.Gateways.WebAPI.Attributes
{
    public class AuthorizeAsyncAttribute : Attribute, IAsyncActionFilter
    {
        private const string AuthoriationHeader = "Authorization";

        private readonly HttpClient http;
        private readonly ServicesEndpoints endpoints;

        public AuthorizeAsyncAttribute(IOptions<ServicesEndpoints> servicesEndpoints)
        {
            this.http = new HttpClient();
            this.endpoints = servicesEndpoints.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isAuthorized = await this.IsAuthorized(context.HttpContext.Request.Headers);
            
            if (isAuthorized)
            {
                await next.Invoke();
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private async Task<bool> IsAuthorized(IHeaderDictionary headers)
        {
            var hasAuthorizationHeader = headers.TryGetValue(AuthoriationHeader, out StringValues headerValues);
            
            if (!hasAuthorizationHeader)
            {
                return false;
            }

            var token = headerValues[0];
            this.SetAuthorizationHeader(token);

            var response = await this.http.GetAsync($"{this.endpoints.Auth}/auth/validate");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }

        private void SetAuthorizationHeader(string headerValue)
        {
            var headers = this.http.DefaultRequestHeaders;

            if (headers.Contains(AuthoriationHeader))
            {
                headers.Remove(AuthoriationHeader);
            }

            headers.Add(AuthoriationHeader, headerValue);
        }
    }
}
