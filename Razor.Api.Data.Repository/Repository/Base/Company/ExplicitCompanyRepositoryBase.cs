using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Logging;
using Razor.Api.DataAccess.Connection;

namespace Razor.Api.DataAccess.Repository.Base.Company
{
    public abstract class ExplicitCompanyRepositoryBase
    {
        protected ILogger<ConfigRepositoryBase> Logger              { get; }
        protected ICompanyDbConnectionFactory   DbConnectionFactory { get; }
        protected ExplicitCompanyRepositoryBase(ICompanyDbConnectionFactory dbConnectionFactory, ILogger<ConfigRepositoryBase> logger)
        {
            DbConnectionFactory = dbConnectionFactory;
            Logger              = logger;
        }

        protected void Exec(long companyId, string query, object param = null)
        {
            using (var c = DbConnectionFactory.GetDbConnection(companyId))
            {
                Logger.LogDebug("{Query} {Params}", query, param);
                c.Execute(query, param: param);
            }
        }

        protected IEnumerable<T> ExecQuery<T>(long companyId, string query, object param = null)
        {
            using (Logger.BeginScope(query))
            using (var c = DbConnectionFactory.GetDbConnection(companyId))
            {
                Logger.LogDebug("Executing with params: {Params}", param);
                var results = c.Query<T>(query, param: param);
                Logger.LogDebug("Returned {Qty} row(s)", results.Count());
                return results;
            }
        }
    }
}