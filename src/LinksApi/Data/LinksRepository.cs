using Npgsql;
using Dapper;
using LinksApi.Data.Models;

namespace LinksApi.Data;

public class LinksRepository : ILinksRepository
{
    Task<Link> ILinksRepository.CreateShortLinkAsync(string longUrl)
    {
        throw new NotImplementedException();
    }

    Task<Link> ILinksRepository.GetLinkFromLongUrlAsync(string longUrl)
    {
        // SELECT id, short_link, long_url, created_at, updated_at	FROM links;
        throw new NotImplementedException();
    }

    Task<Link> ILinksRepository.GetLinkFromShortLinkAsync(string longUrl)
    {
        throw new NotImplementedException();
    }
}
