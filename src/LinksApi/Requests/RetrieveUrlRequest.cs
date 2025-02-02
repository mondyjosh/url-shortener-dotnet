using System.Text.Json.Serialization;

namespace LinksApi.Requests;

// TODO: Document
public class RetrieveUrlRequest
{
    [JsonPropertyName("short_link")]
    public required string ShortLink { get; set; }
}