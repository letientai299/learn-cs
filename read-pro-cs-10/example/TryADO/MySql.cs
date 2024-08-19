using Microsoft.VisualBasic;
using MySqlConnector;

public static class MySql
{
    private const string ConnectionString =
        "Server=localhost;User ID=root;Password=123;Database=mysql";

    internal static void Try()
    {
        using var db = new MySqlConnection(ConnectionString);
        db.Open(); // TODO (tai): need to close the connection?

        using var read = db.CreateCommand();
        read.CommandText = """
            select help_topic_id, name, help_category_id
            from mysql.help_topic 
            limit 20;
            """;

        using var data = read.ExecuteReader();
        var row = new object[2];
        const int catColWidth = 30;
        WriteLine($"id  | {"category", -catColWidth} | name");
        WriteLine($"--- | {"---", -catColWidth} | ---");
        while (data.Read())
        {
            data.GetValues(row);
            WriteLine($"{row[0], 3} | {row[1], -catColWidth} | {row[1]}");
        }
    }
}
