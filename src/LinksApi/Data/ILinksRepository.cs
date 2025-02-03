using LinksApi.Data.Models;

namespace LinksApi.Data;

/// <summary>
/// Data access repository for links.
/// </summary>
interface ILinksRepository
{
    /// <summary>
    /// Fetches a link by a long URL.
    /// </summary>
    /// <param name="longUrl">The long URL used to search for existing shortlinks.</param>
    /// <returns>A <see cref="Link"/> object if found, otherwise <c>null</c>.</returns>
    public Task<Link?> GetLinkFromLongUrlAsync(string longUrl);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shortLink"></param>
    /// <returns></returns>
    public Task<Link?> GetLinkFromShortLinkAsync(string shortLink);
    public Task<Link> CreateShortLinkAsync(string shortLink, string longUrl);
}
