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
        _db = new NpgsqlConnection(temp_cxnstring);
    }

    /*async*/
    Task<Link> ILinksRepository.CreateShortLinkAsync(string longUrl)
    {
        // build the Link model, Insert and return int?
        throw new NotImplementedException();

        /*
    await using SqlConnection cn = new(ConnectionString());
    await using SqlCommand cmd = new()
    {
        Connection = cn,
        CommandText = SqlStatements.InsertPeople
    };

    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = person.Id;
    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = person.FirstName;
    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = person.LastName;
    cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = person.BirthDate;

    await cn.OpenAsync();

    person.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());


        */
    }
    
    async Task<Link?> ILinksRepository.GetLinkFromLongUrlAsync(string longUrl)
    {
        using var cxn = new NpgsqlConnection(temp_cxnstring);

        var query = "SELECT id, short_link, long_url, created_at, updated_at FROM links WHERE long_url =@longUrl ";

        return await _db.QuerySingleOrDefaultAsync<Link>(query, new { longUrl });
    }

    async Task<Link?> ILinksRepository.GetLinkFromShortLinkAsync(string shortLink)
    {
        using var cxn = new NpgsqlConnection(temp_cxnstring);

        var query = "SELECT id, short_link, long_url, created_at, updated_at FROM links WHERE short_link =@shortLink ";

        return await _db.QuerySingleOrDefaultAsync<Link>(query, new { shortLink });
    }

    private readonly IDbConnection _db;
    private const string temp_cxnstring = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;";
}
