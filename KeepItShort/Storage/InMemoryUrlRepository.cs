using KeepItShort.Storage.Interfaces;
using System.Collections.Concurrent;

namespace KeepItShort.Storage
{
    public class InMemoryUrlRepository : IUrlRepository
    {
        private readonly ConcurrentDictionary<string, string> _urlsByCode;

        public InMemoryUrlRepository()
        {
            _urlsByCode = new(StringComparer.Ordinal);
        }

        public bool TryGetOriginalUrl(string code, out string? originalUrl)
        {
            return _urlsByCode.TryGetValue(code, out originalUrl);
        }

        public bool TryAdd(string code, string originalUrl)
        {
            return _urlsByCode.TryAdd(code, originalUrl);
        }
    }
}
