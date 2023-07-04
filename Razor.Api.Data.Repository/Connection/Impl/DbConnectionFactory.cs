using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Razor.Api.DataAccess.Connection.Impl
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DbConnectionFactory> _logger;
        public DbConnectionFactory(IConfiguration configuration, ILogger<DbConnectionFactory> logger)
        {
            _configuration = configuration;
            _logger   = logger;
        }

        public IDbConnection GetDbConnection()
        {
            var connectionString = _configuration.GetConnectionString("ConfigDb");
            _logger.LogDebug("Connecting to {ConnectionString}", connectionString);
            return new SqlConnection(connectionString);
        }
    }
}