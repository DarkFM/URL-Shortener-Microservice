using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Validators
{
    public sealed class Url : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var url = value.ToString();
            if (IsValidUriString(url) && IsValidSiteAsync(url).GetAwaiter().GetResult())
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("invalid URL");
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
