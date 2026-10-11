namespace KeepItShort.Models.Requests
{
    public class CreateShortUrlRequest
    {
        public required string OriginalUrl { get; set; }
    }
}
