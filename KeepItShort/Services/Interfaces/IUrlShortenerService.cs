using KeepItShort.Services.Structs;
using System.Diagnostics.CodeAnalysis;

namespace KeepItShort.Services.Interfaces
{
    public interface IUrlShortenerService
    {
        /// <summary>
        /// Dato un url <paramref name="originalUrl"/>, verifica che sia valido, lo accorcia
        /// in un codice e ne registra l'associazione in un repository.
        /// </summary>
        /// <param name="originalUrl">Url su cui eseguire l'accorciamento</param>
        /// <returns>Un record <see cref="ShortenResult"/> rappresentante l'esito dell'operazione</returns>
        ShortenResult Shorten(string originalUrl);

        /// <summary>
        /// Dato il codice abbreviato di un url (<paramref name="code"/>), recupera l'url originale in <paramref name="originalUrl"/>
        /// </summary>
        /// <param name="code">Codice abbreviato di un url</param>
        /// <param name="originalUrl">Url originale associato all'abbreviato <paramref name="code"/></param>
        /// <returns>True se ha trovato l'associazione, altrimenti false</returns>
        bool TryGetOriginalUrl(string code, [NotNullWhen(true)] out string? originalUrl);
    }
}
