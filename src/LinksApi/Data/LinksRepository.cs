using Npgsql;
using Dapper;
using LinksApi.Data.Models;
using System.Data;

namespace LinksApi.Data;

class LinksRepository : ILinksRepository
{
    public LinksRepository(/*IConfiguration config*/)
    {
        // TODO: DI config
        // _db = new NpgsqlConnection(config.GetConnectionString("Employeedb"));
        // _db = new NpgsqlConnection(temp_cxnstring);

        _connectionString = temp_cxnstring;
    }

    /*async*/
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

    private readonly string _connectionString;
    private const string temp_cxnstring = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;";
}
