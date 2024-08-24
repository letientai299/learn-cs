using Npgsql;

namespace Example.TryADO;

internal static class Pg
{
    private const string ConnectionString =
        "Host=localhost;Username=postgres;Password={0};Database=postgres";

    internal static void Try(string pw)
    {
        var db = NpgsqlDataSource.Create(string.Format(ConnectionString, pw));

        var initSqlPath = FindUp("init.sql");
        Log(initSqlPath);
        RunFile(db, initSqlPath);

        var insert = db.CreateCommand(
            """
            insert into messages(id, created_at) 
            values (@id, @created);
            """
        );

        insert.Parameters.AddWithValue("id", "msg with named param");
        insert.Parameters.AddWithValue("created", DateTime.Now);
        insert.ExecuteNonQuery();

        var read = db.CreateCommand("select * from messages limit 10;");

        var data = read.ExecuteReader();
        var columnCount = data.GetColumnSchema().Count;

        var row = 0;
        while (data.Read())
        {
            row++;
            for (var col = 0; col < columnCount; col++)
            {
                var val = data.GetValue(col);
                Log(row, col, val);
            }
        }
    }

    private static void RunFile(NpgsqlDataSource db, string filePath) =>
        db.CreateCommand(File.ReadAllText(filePath)).ExecuteNonQuery();
}
