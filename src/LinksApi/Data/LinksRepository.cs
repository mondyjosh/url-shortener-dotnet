using Dapper;
using LinksApi.Data.Models;
using Microsoft.Extensions.Configuration;
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

    async Task<Link?> ILinksRepository.GetLinkFromLongUrlAsync(string longUrl) => await GetLinkAsync("long_url", longUrl);

    async Task<Link?> ILinksRepository.GetLinkFromShortLinkAsync(string shortLink) => await GetLinkAsync("short_link", shortLink);

    private async Task<Link?> GetLinkAsync(string column, string value)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var sql = $@"
            SELECT
                id,
                short_link,
                long_url,
                created_at,
                updated_at
            FROM
                links
            WHERE 
                {column} = @value
        ";

        return await connection.QuerySingleOrDefaultAsync<Link>(sql, new { value });
    }

    private readonly string _connectionString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Missing ConnectionStrings:DefaultConnection");
}
