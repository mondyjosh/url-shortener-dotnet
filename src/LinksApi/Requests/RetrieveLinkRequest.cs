using System.Text.Json.Serialization;

namespace LinksApi.Requests;

public class RetrieveLinkRequest
{
    [JsonPropertyName("short_url")]
    public required string ShortUrl { get; set; }
}