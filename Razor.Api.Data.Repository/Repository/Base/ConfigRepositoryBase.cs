using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Logging;
using Razor.Api.DataAccess.Connection;

namespace Razor.Api.DataAccess.Repository.Base
{
    public abstract class ConfigRepositoryBase
    {
        protected ILogger<ConfigRepositoryBase> Logger            { get; }
        protected IDbConnectionFactory          ConnectionFactory { get; }
        protected ConfigRepositoryBase(IDbConnectionFactory connectionFactory, ILogger<ConfigRepositoryBase> logger)
        {
            ConnectionFactory = connectionFactory;
            Logger       = logger;
        }
        
        protected void Exec(string query, object param = null)
        {
            using (var c = ConnectionFactory.GetDbConnection())
            {
                Logger.LogDebug("{Query} {Params}", query, param);
                c.Execute(query, param: param);
            }
        }

        protected IEnumerable<T> ExecQuery<T>(string query, object param = null)
        {
            using (Logger.BeginScope(query))
            using (var c = ConnectionFactory.GetDbConnection())
            {
                Logger.LogDebug("Executing with params: {Params}", param);
                var results = c.Query<T>(query, param: param);
                Logger.LogDebug("Returned {Qty} row(s)", results.Count());
                return results;
            }
        }
    }
}