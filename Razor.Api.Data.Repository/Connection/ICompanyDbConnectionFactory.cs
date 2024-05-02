using System.Data;

namespace Razor.Api.DataAccess.Connection
{
    /// <summary>
    /// Gets the company connection.
    /// </summary>
    public interface ICompanyDbConnectionFactory
    {
        IDbConnection GetDbConnection(long companyId);
    }
}
