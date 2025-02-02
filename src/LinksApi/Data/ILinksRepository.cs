using LinksApi.Data.Models;

namespace LinksApi.Data;

// TODO: Document
interface ILinksRepository
{
    public Task<Link> GetLinkFromLongUrlAsync(string longUrl);
    public Task<Link> GetLinkFromShortLinkAsync(string longUrl);
    public Task<Link> CreateShortLinkAsync(string longUrl);
}
