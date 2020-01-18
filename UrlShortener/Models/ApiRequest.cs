using UrlShortener.Domain.Validators;

namespace UrlShortener.Models
{
    public class ApiRequest
    {
        [Url]
        public string WebSite { get; set; }
    }
}
