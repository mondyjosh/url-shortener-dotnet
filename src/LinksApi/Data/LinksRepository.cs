using Microsoft.Extensions.Configuration;

using Dapper;

using LinksApi.Data.Models;

using Npgsql;

namespace LinksApi.Data;

class LinksRepository(IConfiguration config) : ILinksRepository
{
    async Task<Link> ILinksRepository.CreateShortLinkAsync(string shortLink, string longUrl)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            INSERT INTO
                links (short_link, long_url)
            VALUES
                (@ShortLink, @LongUrl)
            RETURNING
                id
        ";

        var link = new Link { ShortLink = shortLink, LongUrl = longUrl };
        var linkId = await connection.ExecuteAsync(sql, link);

        link.Id = linkId;

        return link;
    }

    async Task<Link?> ILinksRepository.GetLinkFromLongUrlAsync(string longUrl)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            SELECT
                id,
                short_link,
                long_url,
                created_at,
                updated_at
            FROM
                links
            WHERE 
                long_url = @longUrl
        ";

        return await connection.QuerySingleOrDefaultAsync<Link>(sql, new { longUrl });
    }

    async Task<Link?> ILinksRepository.GetLinkFromShortLinkAsync(string shortLink)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            SELECT
                id,
                short_link,
                long_url,
                created_at,
                updated_at
            FROM
                links
            WHERE 
                short_link = @shortLink
        ";

        return await connection.QuerySingleOrDefaultAsync<Link>(sql, new { shortLink });
    }

    private readonly string _connectionString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Missing ConnectionStrings:DefaultConnection");    
}
