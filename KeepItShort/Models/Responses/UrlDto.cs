namespace KeepItShort.Models.Responses
{
    public class UrlDto
    {
        public string Code { get; set; }
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }

        public UrlDto()
        {
            Code = ShortUrl = OriginalUrl = string.Empty;
        }
    }
}
