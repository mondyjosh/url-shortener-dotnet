using System.Text.Json.Serialization;

namespace LinksApi.Responses;

// TODO: Document
public class ShortLinkResponse
{
    [JsonPropertyName("short_url")]
    public required string ShortLink { get; set; }
}