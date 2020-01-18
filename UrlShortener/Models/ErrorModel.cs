using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace UrlShortener.Models
{
    public class ErrorModel
    {
        public ErrorModel(ActionContext context)
        {
            var errorMessage = context.ModelState.First();
            Error = errorMessage.Value.Errors.First().ErrorMessage;
        }

        public string Error { get; set; } = "invalid";
    }
}
