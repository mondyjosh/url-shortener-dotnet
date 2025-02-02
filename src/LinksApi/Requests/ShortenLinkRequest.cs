using System.Text.Json.Serialization;

namespace LinksApi.Requests;

public class ShortenLinkRequest
{
    [JsonPropertyName("long_url")]
    public required string LongUrl { get; set; }
}