using System.Diagnostics.CodeAnalysis;

namespace KeepItShort.Storage.Interfaces
{
    public interface IUrlRepository
    {
        /// <summary>
        /// Cerca l'url originale associato a un codice.
        /// </summary>
        /// <param name="code">Codice abbreviato di un url.</param>
        /// <param name="originalUrl">Url originale in output associato all'abbreviato <paramref name="code"/></param>
        /// <returns>True se esiste altrimenti false</returns>
        bool TryGetOriginalUrl(string code, [NotNullWhen(true)] out string? originalUrl);

        /// <summary>
        /// Salva la'associazione codice url accorciato (<paramref name="code"/>) -> url originale (<paramref name="originalUrl"/>)
        /// in un repository.
        /// </summary>
        /// <param name="code">Codice abbreviato di un url.</param>
        /// <param name="originalUrl">Url originale associato all'abbreviato <paramref name="code"/></param>
        /// <returns>True se è stato aggiunto altrimenti flase</returns>
        bool TryAdd(string code, string originalUrl);
    }
}
