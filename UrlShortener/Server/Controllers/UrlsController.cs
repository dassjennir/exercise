using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain.Services.Interfaces;
using UrlShortener.Server.Filters;
using UrlShortener.Shared.SharedModels;

namespace UrlShortener.Server.Controllers
{
    
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly ILogger<UrlsController> _logger;
        private IUrlService _urlService;

        public UrlsController(ILogger<UrlsController> logger, IUrlService urlService)
        {
            _logger = logger;
            _urlService = urlService;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<UrlPairDTO>> GetAll()
        {
            var pairs = (await _urlService.GetAllUrlPairsAsync()).Select(p => new UrlPairDTO
            {
                LongUrl = p.LongUrl,
                ShortUrl = p.ShortUrlCode
            });
            return pairs;
        }

        [ShortUrlLimitOverflowHandler]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateShortUrl(CreateUrlRequestDTO req)
        {
            string shortUrlCode = await _urlService.CreateShortUrlAsync(req.LongUrl); // may throw, but no problem, exception filter will handle
            return CreatedAtAction(nameof(RedirectToRealUrl), new { shortUrlCode }, new { shortUrlCode });
        }

        // This action method doesn't seem to fit here as it should be in another controller. Let here to keep things simple and moved [Route]
        // to action methods.
        [HttpGet]
        [Route("{shortUrlCode}")]
        public async Task<IActionResult> RedirectToRealUrl(string shortUrlCode)
        {

            if (!_urlService.IsCodeStructureValid((string)shortUrlCode)) // validation moved here, because it's a simple string and it's from route.
            {
                return BadRequest($"{shortUrlCode} is not a proper short URL ID");
            }

            string realUrl = await _urlService.GetLongUrlAsync((string)shortUrlCode);

            if (realUrl == null)
            {
                return NotFound($"Website with short link {shortUrlCode} not found");
            }
            return Redirect(realUrl);
        }
    }
}
