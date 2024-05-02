using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Razor.Api.Context;
using Razor.Api.DataAccess.Connection;

namespace Razor.Api.DataAccess.Repository.Base.Company
{
    public abstract class CompanyRepositoryBase: ExplicitCompanyRepositoryBase
    {
        protected IExecContext ExecContext { get; }

        protected CompanyRepositoryBase(ICompanyDbConnectionFactory dbConnectionFactory, IExecContext execContext, ILogger<ConfigRepositoryBase> logger) : base(dbConnectionFactory, logger)
        {
            ExecContext = execContext;
        }

        protected void Exec(string query, object param = null)
            => Exec(ExecContext.Data.CompanyId, query, param);

        protected IEnumerable<T> ExecQuery<T>(string query, object param = null)
            => ExecQuery<T>(ExecContext.Data.CompanyId, query, param);
    }
}
