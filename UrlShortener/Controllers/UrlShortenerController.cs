using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Repositories;
using UrlShortener.Filters;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/shorturl")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly ILogger<UrlShortenerController> _logger;
        private readonly ISiteRepository _siteRepository;

        public UrlShortenerController(ILogger<UrlShortenerController> logger, ISiteRepository siteRepository)
        {
            _logger = logger;
            _siteRepository = siteRepository;
        }

        [HttpPost("new")]
        [ValidUrl]
        public async Task<IActionResult> AddUrlAsync([FromForm(Name = "url")]string website)
        {
            var site = await _siteRepository.GetByUrl(website);
            if (site == null)
            {
                site = _siteRepository.Add(new Website { Url = website });
                await _siteRepository.UnitOfWork.SaveChangesAsync();
            }

            return Ok(new ApiResponse
            {
                OriginalUrl = site.Url,
                ShortUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/api/shorturl/{site.Id}"
            });
        }

        [HttpGet("{siteId}")]
        public async Task<IActionResult> RedirectToSite(int siteId)
        {
            var site = await _siteRepository.GetByIdAsync(siteId);
            if (site == null)
                return NotFound(new ErrorModel("invalid URL"));

            return Redirect(site.Url);
        }
    }
}
