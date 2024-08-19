// This is an experiment with low level DB driver.

DotNetEnv.Env.TraversePath().Load();

var pw = DotNetEnv.Env.GetString("DUMMY_PW", "123");

Header("Postgres");
Pg.Try(pw);

Header("MySQL");
MySql.Try(pw);

Header("SQL Server");
SqlServer.Try(DotNetEnv.Env.GetString("DUMMY_PW_SS"));
return;

static void Header(string s) =>
    WriteLine(
        $"\n\n{s, -10} {string.Concat(Enumerable.Repeat("-", 60).ToArray())}\n"
    );
