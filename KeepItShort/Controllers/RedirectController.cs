using KeepItShort.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeepItShort.Controllers
{
    /// <summary>
    /// Endpoint utilizzato per il reindirizzamento da codice url abbreviato a url originale.
    /// </summary>
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly IUrlShortenerService _shortenerService;

        public RedirectController(IUrlShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        /// <summary>
        /// Dato il codice abbreviato <paramref name="code"/>, effettua il redirect vero e proprio
        /// associato all'url originale.
        /// </summary>
        /// <param name="code">Url abbreviato di partenza</param>
        [HttpGet("{code}")]
        public IActionResult RedirectToOriginal(string code)
        {
            if (!_shortenerService.TryGetOriginalUrl(code, out var originalUrl))
            {
                return NotFound();
            }

            return Redirect(originalUrl);
        }
    }
}
