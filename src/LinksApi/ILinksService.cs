using LinksApi.Requests;
using LinksApi.Responses;

namespace LinksApi;

/// <summary>
/// Service that provides link management functionality.
/// </summary>
public interface ILinksService
{
    /// <summary>
    /// Creates a short link.
    /// </summary>
    /// <param name="request">The <see cref="ShortLinkRequest"/> object that supplies the long URL to shorten.</param>
    /// <returns>A shortened link representing the original long URL.</returns>
    public Task<ShortLinkResponse> ShortenLinkAsync(ShortLinkRequest request);

    /// <summary>
    /// Retrieves an original URL from short link.
    /// </summary>
    /// <param name="request">The <see cref="RetrieveUrlRequest"/> object that supplies the shortLink used to retrieve the original URL.</param>
    /// <returns>The original long URL.</returns>
    public Task<RetrieveUrlResponse> RetrieveUrlAsync(RetrieveUrlRequest request);
}
