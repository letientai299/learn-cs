// This is an experiment with low level DB driver.

using DotNetEnv;
using Example.TryADO;

Env.TraversePath().Load();

var pw = Env.GetString("DUMMY_PW", "123");

Header("Postgres");
Pg.Try(pw);

Header("MySQL");
MySql.Try(pw);

Header("SQL Server");
SqlServer.Try(Env.GetString("DUMMY_PW_SS"));
