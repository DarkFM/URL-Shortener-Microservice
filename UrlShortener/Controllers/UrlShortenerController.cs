using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/shorturl")]
    public class UrlShortenerController : ControllerBase
    {

        private readonly ILogger<UrlShortenerController> _logger;
        private readonly IWebHostEnvironment _env;

        public UrlShortenerController(ILogger<UrlShortenerController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        [HttpGet("new")]
        public IActionResult StoreUrl([FromQuery]ApiRequest request)
        {
            return Ok(request.WebSite);
        }



        private class ApiResponse
        {
            public string OriginalUrl { get; set; }
            public int ShortUrl { get; set; }
        }
    }
}
