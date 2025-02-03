using System.Text.Json.Serialization;

namespace LinksApi.Responses;

/// <summary>
/// Response of a long URL retrieval.
/// </summary>
public class RetrieveUrlResponse
{
    /// <summary>
    /// The long URL related to the short link.
    /// </summary>
    [JsonPropertyName("long_url")]
    public required string LongUrl { get; set; }

    /// <summary>
    /// The short link representing the long URL.
    /// </summary>
    [JsonPropertyName("short_link")]
    public required string ShortLink { get; set; }
}