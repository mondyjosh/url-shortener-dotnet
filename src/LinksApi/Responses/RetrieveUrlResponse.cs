using System.Text.Json.Serialization;

namespace LinksApi.Responses;

// TODO: Document
public class RetrieveUrlResponse
{
    [JsonPropertyName("long_url")]
    public required string LongUrl { get; set; }

    [JsonPropertyName("short_link")]
    public required string ShortLink { get; set; }
}