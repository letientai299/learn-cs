// This is an experiment with low level DB driver.

using System.Threading.Channels;

WriteLine("postgres ----------------------------------------");
Pg.Try();

WriteLine("mysql    ----------------------------------------");
MySql.Try();
