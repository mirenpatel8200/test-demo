using System;
using System.Data;

namespace Razor.Api.ConnectionFactory
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectionFactory
    {
     
        /// <summary>
        /// Gets the tenant connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        IDbConnection GetDatabaseConnection(long companyId);

    }
}
