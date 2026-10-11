using KeepItShort.Services.Interfaces;
using KeepItShort.Services.Structs;
using KeepItShort.Storage.Interfaces;

namespace KeepItShort.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int CodeLength = 7;

        private readonly IUrlRepository _repository;

        public UrlShortenerService(IUrlRepository repository)
        {
            _repository = repository;
        }

        public ShortenResult Shorten(string originalUrl)
        {
            string normalizedUrl, urlShortCode;

            if (
                    !Uri.TryCreate(originalUrl, UriKind.Absolute, out var uri) ||
                    (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
               )
            {
                return new ShortenResult(ShortenStatus.InvalidUrl, null);
            }

            //MG: normalizzazione uri
            normalizedUrl = uri.AbsoluteUri;

            do
            {
                urlShortCode = GenerateCode();
            }
            while (!_repository.TryAdd(urlShortCode, normalizedUrl));

            return new ShortenResult(ShortenStatus.Created, urlShortCode);
        }

        public bool TryGetOriginalUrl(string code, out string? originalUrl)
        {
            return _repository.TryGetOriginalUrl(code, out originalUrl);
        }

        private static string GenerateCode()
        {
            return new(Random.Shared.GetItems<char>(Alphabet, CodeLength));
        }
    }
}
