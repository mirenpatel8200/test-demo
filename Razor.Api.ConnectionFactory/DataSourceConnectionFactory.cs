using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Razor.Api.ConnectionFactory
{
    public class DataSourceConnectionFactory : IConnectionFactory
    {
        public IDbConnection GetDatabaseConnection(long companyId)
        {
            throw new NotImplementedException();
        }
    }
}
