using System.Text.Json.Serialization;

namespace LinksApi.Requests;

// TODO: Document
public class ShortLinkRequest(string longUrl)
{
    [JsonPropertyName("long_url")]
    public required string LongUrl { get; set; } = longUrl;
}