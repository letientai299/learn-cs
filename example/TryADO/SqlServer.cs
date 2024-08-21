using System.Data;
using Microsoft.Data.SqlClient;

internal static class SqlServer
{
    private const string ConnectionString =
        // important: can't connect to localhost, must use IP.
        "Server=127.0.0.1,1433;"
        + "Database=msdb;"
        + "User Id=sa;"
        + "Password={0};"
        + "TrustServerCertificate=True;"
        + "Encrypt=False;";

    internal static void Try(string pw)
    {
        using var db = new SqlConnection(string.Format(ConnectionString, pw));
        db.Open(); // TODO (tai): need to close the connection?

        using var read = db.CreateCommand();

        // write the sql like this to remind myself
        // that SQL server doesn't support typical LIMIT.
        read.CommandText = """
            select version_string, script_hash
            from msdb_version
            order by id
            offset 0 rows fetch first 1 rows only;
            """;

        using var data = read.ExecuteReader();
        if (!data.Read())
        {
            throw new VersionNotFoundException("can't read SQL Server version");
        }

        var row = new object[2];
        data.GetValues(row);
        WriteLine($"version={row[0]}, hash={row[1]}");
    }
}
