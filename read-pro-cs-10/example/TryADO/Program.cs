// This is an experiment with low level DB driver.

DotNetEnv.Env.TraversePath().Load();

var pw = DotNetEnv.Env.GetString("DUMMY_PW", "123");

Header("Postgres");
Pg.Try(pw);

Header("MySQL");
MySql.Try(pw);

Header("SQL Server");
SqlServer.Try(DotNetEnv.Env.GetString("DUMMY_PW_SS"));
