using Npgsql;

namespace ContactPro.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        { 
            var connectionString = configuration.GetSection("pgSettings")["pgConnection"]; 
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
             
            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        //build a connection string from the environment. i.e. Railway
        private static string BuildConnectionString(string databaseUrl)
        {
            var databseUri = new Uri(databaseUrl);
            var userInfo = databseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databseUri.Host,
                Port = databseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            return builder.ToString();
        }
    }
}
