// using Npgsql;
// using Dapper;

// namespace LinksApi.Data;

// public class LinksRepository : ILinksRepository
// {
//     public LinksRepository(string connectionString)
//     {
//         connection = new NpgsqlConnection(connectionString);
//         connection.Open();
//     }

//     public async Task AddShortLink(string shortLink)
//     {
//         string commandText = $"INSERT INTO 
//          (id, Name, MinPlayers, MaxPlayers, AverageDuration) VALUES (@id, @name, @minPl, @maxPl, @avgDur)";

//         await using var cmd = new NpgsqlCommand(commandText, connection);

//         cmd.Parameters.AddWithValue("id", game.Id);
//         cmd.Parameters.AddWithValue("name", game.Name);
//         cmd.Parameters.AddWithValue("minPl", game.MinPlayers);
//         cmd.Parameters.AddWithValue("maxPl", game.MaxPlayers);
//         cmd.Parameters.AddWithValue("avgDur", game.AverageDuration);

//         await cmd.ExecuteNonQueryAsync();
//     }

//     public async GetLinkAsync()
//     {
//         connection.Query<Dog>("select Age = @Age, Id = @Id", new { Age = (int?)null, Id = guid });
//     }

//     private NpgsqlConnection connection;



// }

