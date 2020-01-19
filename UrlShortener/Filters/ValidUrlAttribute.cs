using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Filters
{
    public class ValidUrlAttribute : TypeFilterAttribute
    {
        public ValidUrlAttribute() : base(typeof(ValidUrlImpl))
        {
        }

        private class ValidUrlImpl : IAsyncActionFilter
        {
            private readonly HttpClient _client;

            public ValidUrlImpl(IHttpClientFactory clientFactory)
            {
                _client = clientFactory.CreateClient();
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.ActionArguments.ContainsKey("website"))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var url = context.ActionArguments["website"] as string;
                if (!IsValidUriString(url) || !(await IsValidSiteAsync(url)))
                {
                    context.Result = new BadRequestObjectResult(new ErrorModel("invalid Url"));
                    return;
                }

                await next();
            }

            private async Task<bool> IsValidSiteAsync(string url)
            {
                using var client = new HttpClient();
                Uri newUrl = new Uri(url);

                var response = await client.GetAsync(newUrl);
                if (!response.IsSuccessStatusCode)
                    return false;

                var ip = await System.Net.Dns.GetHostEntryAsync(newUrl.DnsSafeHost);
                return ip.AddressList.Length != 0;
            }

            private bool IsValidUriString(string url)
            {
                return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
        }
    }
}
