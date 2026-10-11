namespace KeepItShort.Services.Structs
{
    public enum ShortenStatus
    {
        Created, 
        InvalidUrl
    }

    public record ShortenResult(ShortenStatus Status, string? Code);
}
