using KeepItShort.Models.Requests;
using KeepItShort.Models.Responses;
using KeepItShort.Services.Interfaces;
using KeepItShort.Services.Structs;
using Microsoft.AspNetCore.Mvc;

namespace KeepItShort.Controllers
{
    /// <summary>
    /// Controller che gestisce le richieste HTTP per l'accorciamento degli url 
    /// e il recupero dell'originale a partire dalla risorsa accorciata.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlShortenerService _shortenerService;

        public UrlsController(IUrlShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        /// <summary>
        /// Dato il codice abbreviato di un url <paramref name="code"/>, restituisce l'url originale.
        /// </summary>
        /// <param name="code">Codice abbreviato di un URL</param>
        /// <returns>Un DTO deserializzabile di risposta contenente l'url originale</returns>
        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UrlDto> GetOriginalUrl(string code)
        {
            UrlDto response;

            if (!_shortenerService.TryGetOriginalUrl(code, out var originalUrl))
            {
                return NotFound();
            }

            response = new UrlDto()
            {
                Code = code,
                ShortUrl = $"{Request.Scheme}://{Request.Host}/{code}",
                OriginalUrl = originalUrl
            };

            return Ok(response);
        }

        /// <summary>
        /// Dati i parametri del body della richiesta HTTP in <paramref name="request"/>,
        /// costruisce un url accorciato.
        /// </summary>
        /// <param name="request">Richiesta HTTP di riferimento</param>
        /// <returns>Un DTO deserializzabile di risposta contenente l'url accorciato, l'originale e la risorsa</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UrlDto> ShortenUrl([FromBody] CreateShortUrlRequest request)
        {
            ShortenResult shortenResult;

            shortenResult = _shortenerService.Shorten(request.OriginalUrl);


            return shortenResult.Status switch
            {
                ShortenStatus.Created => CreatedAtAction
                (
                    nameof(GetOriginalUrl),
                    new { code = shortenResult.Code },
                    new UrlDto()
                    {
                        Code = shortenResult.Code!,
                        ShortUrl = $"{Request.Scheme}://{Request.Host}/{shortenResult.Code}",
                        OriginalUrl = request.OriginalUrl
                    }
                ),

                ShortenStatus.InvalidUrl => Problem
                (
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: "il formato dell'url non è valido"
                )
            };
        }
    }
}
